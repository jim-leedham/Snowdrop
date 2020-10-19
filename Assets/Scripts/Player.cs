using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

namespace Snowdrop
{
    public class Player : MonoBehaviour
    {
        public float moveVelocity = 1.0f;
        public float jumpVelocity;

        private Rigidbody2D rigidbody2d;

        public GameObject animatorObject;

        private Animator animator;

        private Vector3 previousPosition;

        private bool jump;

        public Transform shoulder;

        public float maxArmReach = 2.4f;
        public float minArmReach = 1.1f;


        private Vector3 destination;


        [SerializeField] private Texture2D crosshairCursorTexture = null;

        public bool Searching { get; private set; }
        public bool Reaching { get; private set; }

        private Vector3 doorOffset = Vector3.zero;
        private Bounds roomBounds;

        [SerializeField] private SpriteResolver leftEye = null;
        [SerializeField] private SpriteResolver rightEye = null;
        [SerializeField] private SpriteResolver mouth = null;

        private float blinkCountdown;
        private float eyesOpenCountdown;
        private bool speaking;
        private float mouthCountdown;

        [SerializeField]
        private SpeechBubbleScript speechBubble = null;

        void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            animator = animatorObject.GetComponent<Animator>();

            //Speech.Instance.SetSpeechBubble(GetComponentInChildren<SpeechBubble>()); //probably a nicer way to do this!
            //Speech.Instance.Disable();

            Searching = false;
            Reaching = false;

            blinkCountdown = Random.Range(2.0f, 5.0f);
            speaking = false;
        }

        void Update()
        {
            UpdateCamera();

            blinkCountdown -= Time.deltaTime;
            if(blinkCountdown < 0.0f)
            {
                leftEye.SetCategoryAndLabel("EyesClosed", leftEye.GetLabel());
                rightEye.SetCategoryAndLabel("EyesClosed", rightEye.GetLabel());
                eyesOpenCountdown = 0.1f;
                blinkCountdown = Random.Range(2.0f, 4.0f);
            }
            if(eyesOpenCountdown > 0.0f)
            {
                eyesOpenCountdown -= Time.deltaTime;
                if (eyesOpenCountdown < 0.0f)
                {
                    leftEye.SetCategoryAndLabel("EyesOpen", leftEye.GetLabel());
                    rightEye.SetCategoryAndLabel("EyesOpen", rightEye.GetLabel());
                }
            }

            if(speaking)
            {
                mouthCountdown -= Time.deltaTime;
                if(mouthCountdown < 0.0f)
                {
                    // randomly alternate between our open-mouth sprites
                    mouth.SetCategoryAndLabel("Mouth", (Random.Range(0.0f, 1.0f) > 0.5f ? "open-d" : "open-o"));
                    mouthCountdown = 0.05f;
                }
            }
            

            // Handle any jumps
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("jump");
            }

            // Handle any horizontal input
            float horizontalMovement = UnityEngine.Input.GetAxis("Horizontal");
            rigidbody2d.velocity = new Vector2(horizontalMovement * moveVelocity, rigidbody2d.velocity.y);

            // Quick and dirty flip in X axis, but long term should do better (i.e. character's hair flick should not change direction!)
            if (Mathf.Abs(horizontalMovement) > float.Epsilon)
            {
                animatorObject.transform.localScale = new Vector3((horizontalMovement > 0.0f ? -1.0f : 1.0f), 1.0f, 1.0f);
            }

            animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
            if (horizontalMovement != 0)
            {
                //Speech.Instance.Disable();
            }

            // Handle our search mode (set the cursor to a crosshair)
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                Cursor.SetCursor(crosshairCursorTexture, new Vector2(32.0f, 32.0f), CursorMode.Auto);
                Searching = true;
            }
            else if (UnityEngine.Input.GetKeyUp(KeyCode.Q))
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                Searching = false;
            }

            // Handle arm movement
            if (UnityEngine.Input.GetKey(KeyCode.E))
            {
                Reaching = true;
                animator.SetBool("reaching", Reaching);
                Vector3 p = UnityEngine.Input.mousePosition;
                Vector3 pos = UnityEngine.Camera.main.ScreenToWorldPoint(p);
                pos.z = shoulder.position.z;
                Vector3 shoulders = shoulder.position;// + new Vector3(0.0f, 3.36f, 0.0f);
                Vector3 mouseDir = shoulders - pos;


                float x = Mathf.InverseLerp(minArmReach, maxArmReach, Mathf.Abs(mouseDir.x));
                //x = mouseDir.x > 0.0f ? x : -x;
                x = Mathf.Abs(mouseDir.x) < minArmReach ? 0.001f : x;
                animator.SetFloat("arm-x", -x);


                float y = Mathf.InverseLerp(0.0f, maxArmReach, Mathf.Abs(mouseDir.y));
                y = mouseDir.y > 0.0f ? -y : y;
                animator.SetFloat("arm-y", y);
                //Debug.Log("X: " + x + " Y: " + y);
            }
            else
            {
                Reaching = false;
                animator.SetBool("reaching", Reaching);
            }

            if ((Reaching || Searching) && UnityEngine.Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("interactables"));
                if (hit.collider != null && hit.collider.gameObject != null)
                {
                    Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
                    if(Searching)
                    {
                        //interactable.LookAt();
                        speechBubble.Display(interactable.Description());
                    }
                    else if(interactable.Touching())
                    {
                        switch(interactable.GetInteractableType())
                        {
                            case InteractableType.PickUp:
                                {
                                    //((Item)interactable).PickUp();
                                    break;
                                }
                            case InteractableType.Door:
                                {
                                    doorOffset = transform.position - interactable.transform.position;
                                    ((Door)interactable).Open();
                                    break;
                                }
                        }
                    }
                }
            }
        }

        private void UpdateCamera()
        {
            // Make the camera track our player
            Vector3 camPos = UnityEngine.Camera.main.transform.position;
            camPos.x = transform.position.x;
            camPos.y = transform.position.y + 4.5f;

            // ensure camera doesn't track past the edge of the room!
            float cameraHeight = 2.0f * UnityEngine.Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * UnityEngine.Camera.main.aspect;
            camPos.x = Mathf.Clamp(camPos.x, roomBounds.min.x + (cameraWidth / 2.0f), roomBounds.max.x - (cameraWidth / 2.0f));

            UnityEngine.Camera.main.transform.position = camPos;
        }

        public void Jump()
        {
            rigidbody2d.AddForce(new Vector2(0.0f, jumpVelocity));
        }

        public void EnterRoom()
        {
            if(doorOffset != Vector3.zero)
            {
                transform.position = Game.Instance.Room.GetDoorPos() + doorOffset;
            }
            roomBounds = Game.Instance.Room.GetExtents().bounds;

            UpdateCamera();
        }

        public void BeginSpeaking()
        {
            speaking = true;
            mouthCountdown = 0.05f;
        }

        public void FinishSpeaking()
        {
            speaking = false;
            mouth.SetCategoryAndLabel("Mouth", "closed");
        }

    }
}
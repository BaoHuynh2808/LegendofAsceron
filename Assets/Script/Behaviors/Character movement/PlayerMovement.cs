using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovingObject {
    public static PlayerMovement instance = null;
	private Rigidbody rigid;
	private Animator ani;
	private CharacterController characterController;
	//[SerializeField] private Transform cameraT;
	[SerializeField] private float inputDelay = 0.1f;
	[SerializeField] private float forwardVel = 12;
	[SerializeField] private float rotateVel = 100;
	private Quaternion targetRotation;
	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed = 3.0f;
	[SerializeField] private float turnSmoothTime;
	private float turnSmoothVelocity;
	public float speedSmoothTime = 0.01f;
    float speedSmoothVelocity;
    float currentSpeed;
	private bool _isAtSavePoint;
	private bool isPlayerPressAttack;
    
	private void Awake() {
        if (instance == null) {
            instance = this;
        }
        //gameObject.transform.position = DataManager.instance.playerPosition;
	}

	private void Start() {
		targetRotation = transform.rotation;
		rigid = GetComponent<Rigidbody>();
		ani = GetComponent<Animator>();
		//animation.Play("idle");
		characterController = GetComponent<CharacterController>();
		isPlayerPressAttack = false;
        DontDestroyOnLoad(this);
		_isAtSavePoint = false;
        DontDestroyOnLoad(Camera.main);
	}

	public Quaternion TargetRotation {
		get {
			return targetRotation;
		}
	}

	void Update() {
		//GetInput();
		//Turn();
		if (GameManager.instance.isEnemyAttackPlayer == false) {
			if (hInput.GetButton("Interact")) {
				isPlayerPressAttack = true;
                //ani.Play("Attack");
                ani.SetBool("Jump", true);
			}
            else
            {
				if (hInput.GetButton("Accelerate")) {
					currentSpeed = sprintSpeed;
                    ani.SetFloat("Speed", 0.5f);
                    ani.SetBool("Jump", false);
                    //ani.Play("Run");
                }
				else 
                {
					currentSpeed = walkSpeed;
                    ani.SetFloat("Speed", 0.2f);
                    ani.SetBool("Jump", false);
                    //ani.Play("Walk");
                }
				isPlayerPressAttack = false;
				Vector2 input = new Vector2(hInput.GetAxis("HorizontalAxis"), hInput.GetAxis("VerticalAxis"));
				Vector2 inputDir = input.normalized;
				if (inputDir != Vector2.zero) {
                    float targetTempRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
					transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y,
																			   targetTempRotation, ref turnSmoothVelocity, turnSmoothTime);
					Vector3 velocity = transform.forward * currentSpeed;
					characterController.Move(velocity * Time.deltaTime);
				}
				else {
                    ani.SetFloat("Speed", 0);
                    ani.SetBool("Jump", false);
					//ani.PlayQueued("idle", QueueMode.PlayNow);
				}
			}
		}
		else
        {
            ani.SetFloat("Speed", 0);
            //ani.PlayQueued("idle", QueueMode.PlayNow);
        }
		if (_isAtSavePoint == true) {
			if (Input.GetKeyDown(KeyCode.Return)) {
                SaveLoadManager.Instance.Save(false);
                MapTransition.instance.saveText.SetActive(true);
            }
		}
	}

	public Vector2 ReturnPlayerPosition() {
        Vector2 playerPosition = transform.position;
        return playerPosition;
    }

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.layer == 9 && isPlayerPressAttack == true) {
			GameManager.instance.isPlayerAttackEnemy = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Save") {
			_isAtSavePoint = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Save") {
			MapTransition.instance.saveText.SetActive(false);
			_isAtSavePoint = false;
		}
	}
}

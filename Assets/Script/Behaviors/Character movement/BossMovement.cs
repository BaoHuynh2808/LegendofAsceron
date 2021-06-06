using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MovingObject {
	[SerializeField] private float rayLength;
    [SerializeField] private float timeToFollowPlayer;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Light spotLight;
    [SerializeField] private float enemyViewDistance;
    [SerializeField] private LayerMask viewMask;
    //private bool isEnemyAttackPlayer = false;
    private bool avoiding = false;
    private Animator animator;
    private float navigationTime = 0;
    float viewAngle;
    private GameObject player;
    private bool enemySeePlayer = false;
    private Color originalSpotLightColor;
    private int checkpoint = 0;
    private Transform enemy;
    private float currentSpeed;
    private float currentTimeEnemyFollowPlayer;
    private Vector3 enemyTarget;
    private bool isDestroy;

	void Start () {
        if (GameManager.instance.playerNameDestroy.Count > 0) {
            foreach (string name in GameManager.instance.playerNameDestroy) {
                if (name == this.gameObject.name) {
                    Destroy(this.gameObject);
                }
            }
        }
        animator = GetComponent<Animator>();
        currentTimeEnemyFollowPlayer = timeToFollowPlayer;
        enemy = GetComponent<Transform>();
        viewAngle = spotLight.spotAngle;
        originalSpotLightColor = spotLight.color;
        player = GameObject.FindWithTag("Player").gameObject;
    }

	private bool EnemyCanSeePlayer() {
        if (Vector3.Distance(transform.position, player.transform.position) < enemyViewDistance) {
            Vector3 distanceToPlayer = (player.transform.position - transform.position).normalized;
            float angleBetweenEnemyAndPlayer = Vector3.Angle(transform.forward, distanceToPlayer);
            if (angleBetweenEnemyAndPlayer < viewAngle / 2f) {
                if (!Physics.Linecast(transform.position,player.transform.position,viewMask)) {
                    enemySeePlayer = true;
                    return true;
                }
            }
        }
        return false;
    }

	private void ObjectAvoidance(Vector3 target) {
        var dir = (target - enemy.position).normalized;
        RaycastHit hit;
        avoiding = false;
        if (Physics.Raycast(transform.position,transform.forward,out hit,rayLength)) {
            if (hit.transform != transform) {
                if (hit.collider.CompareTag("Object")) {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    dir += hit.normal * 20;
                    avoiding = true;
                }
            }
        }
        var leftRay = transform.position;
        var rightRay = transform.position;
        leftRay.x -= 2;
        rightRay.x += 2;
        if (Physics.Raycast(leftRay,transform.forward,out hit,rayLength)) {
            if (hit.transform != transform) {
                if (hit.collider.CompareTag("Object")) {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    dir += hit.normal * 20;
                    avoiding = true;
                }
            }
        }
        if (Physics.Raycast(rightRay, transform.forward, out hit, rayLength)) {
            if (hit.transform != transform) {
                if (hit.collider.CompareTag("Object")) {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    dir -= hit.normal * 20;
                    avoiding = true;
                }
            }
        }
        var rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
        transform.position += transform.forward * walkSpeed * Time.deltaTime;
    }

	void Update () {
        if (EnemyCanSeePlayer()) {
            spotLight.color = Color.red;
        }
        else {
            spotLight.color = originalSpotLightColor;
        }
        if(GameManager.instance.isEnemyAttackPlayer == true && GameManager.instance.isPlayerAttackEnemy == false) {
            animator.Play("Attack");
        }
        else {
            //if (GameManager.instance.isPlayerAttackEnemy == true) {
            //  //Destroy(this.gameObject);
            //  //GameManager.instance.GoToBattle();
            //  ScreenManager.Instance.TriggerBattleFadeOut();
            //}
            if (enemySeePlayer == true && currentTimeEnemyFollowPlayer >= 0) {
                currentTimeEnemyFollowPlayer -= Time.deltaTime;
                currentSpeed = sprintSpeed;
                animator.SetBool("isWalk", false);
                enemyTarget = player.transform.position;
				ObjectAvoidance(enemyTarget);
            }
        }
    }

	public void attackEvent() {
        
        GameManager.instance.isEnemyAttackPlayer = true;
        GameManager.instance.isPlayerAttackEnemy = false;
        //for (int i = 0; i < MapManager.instance.enemyInMap.Length; i++) {
        //  if (MapManager.instance.enemyInMap[i].name == this.gameObject.name) {
        //      Debug.Log(i);
        //      GameManager.instance.enemyInMapIndex.Add(i);
        //      break;
        //  }
        //}

        //GameManager.instance.GoToBattle();

    }

	private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 8) {
            //if (GameManager.instance.isPlayerAttackEnemy == false && enemySeePlayer == true) {
                
            //}
            GameManager.instance.isEnemyAttackPlayer = true;
			GameManager.instance.isBossFight = true;
            //attackEvent();
			GameManager.instance.playerNameDestroy.Add(this.gameObject.name);
            Destroy(this.gameObject);
            ScreenManager.Instance.TriggerBattleFadeOut();
            //else {
            //  GameManager.instance.isPlayerAttackEnemy = true;
            //}
        }
    }

    private void OnDestroy() {
        ScreenManager.Instance.TriggerBattleFadeOut();
    }
}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public GameObject shield;
    public float shieldGrowSpeed;

    public float speed;
    public float dashSpeed;
    public float dashDistance;

    public int score;

    public float dashCooldown;
    public float blockTime;   

	// Use this for initialization
	void Start () {
        //if (speed <= 0) speed = 1;
        //if (dashSpeed <= 0) dashSpeed = 10;
        //if (dashDistance <= 0) dashDistance = 5;
        score = 0;
        GetComponent<TrailRenderer>().enabled = false;
    }

    public void Dash() {
        dashCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Jump") && dashCooldown <= 0) {
            AnimationManager.OnBeginDash();
            GetComponent<TrailRenderer>().enabled = true;
            //transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * dashSpeed * speed * Time.deltaTime;
            speed = speed * 10;
            dashCooldown = 0.2f;
            dashDistance = .2f;
        }

        if (dashDistance > 0) {
            dashDistance -= Time.deltaTime;
        }

        if (dashDistance <= 0) {
            speed = 5;
        }
        else { AnimationManager.OnDashing(); }
    }
    public void Movement() {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;
    }
    public void Block() {

        if (Input.GetKey(KeyCode.LeftShift)) {
            AnimationManager.OnBeginBlock();
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            AnimationManager.OnBlocking();
            speed = 0;
            shield.SetActive(true);
            shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(2, 2, 2), Time.deltaTime * shieldGrowSpeed);

        }
        else {
            shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * shieldGrowSpeed);
            if (shield.transform.localScale.x <= .2f)
                shield.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        Block();
        Movement();
        Dash();
	}
}
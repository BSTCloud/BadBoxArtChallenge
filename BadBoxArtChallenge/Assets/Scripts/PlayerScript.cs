using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float movementSpeed;
    public float bounceForce;
    public float leftMargin;
    public float rightMargin;

	private Animator ballAnimator;
	private SpriteRenderer ballRenderer;
	private Vector3 velocity;
	private float moving;
	private CircleCollider2D ballCollider;
	private Rigidbody2D ballBody;
	private int bounce;

	private float clicCoeficient;

	private Vector2 defaultVelocity;

	// Use this for initialization
	void Start () {
		ballAnimator = gameObject.GetComponent<Animator> ();
		ballRenderer = gameObject.GetComponent<SpriteRenderer>();
		ballCollider = gameObject.GetComponent<CircleCollider2D> ();
		ballBody = gameObject.GetComponent<Rigidbody2D>();
		this.ballCollider.sharedMaterial.bounciness = 1;
		bounce = 0;
		clicCoeficient = 1.0f;
		UnityEngine.Cursor.visible = false;
		UnityEngine.Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        //float moving = Input.GetAxis("Horizontal");
		moving = Input.GetAxis ("Mouse X");

		if(Input.GetMouseButtonDown (0)){
			ballBody.AddForce(Vector2.down * bounceForce); 
			bounce = 1;
		}

		if(Input.GetMouseButtonDown(1) && gameObject.transform.position.y < -2.5f){
			bounce = 2;
		}

        velocity = new Vector3(moving * movementSpeed, 0, 0);
		if (velocity.x < 0){
			ballRenderer.flipX = true;
		}
		else if (velocity.x > 0){
			ballRenderer.flipX = false;
		}

        if (moving < 0 && transform.position.x < leftMargin || moving > 0 && transform.position.x > rightMargin){
            velocity.x = 0;
        }

		transform.Translate(velocity * clicCoeficient * Time.deltaTime);

        // cambiar la dirección
        
        
	}

	void OnCollisionEnter2D(Collision2D itemCollider)
	{
		if (itemCollider.gameObject.CompareTag("Floor")){
			this.ballAnimator.CrossFade ("ballBounceAnim", 0);

			//Debug.Log (ballBody.velocity);

			switch(bounce){
			case 0:
				ballBody.velocity = new Vector2 (0.0f, 18.1f);
				clicCoeficient = 1.0f;
				break;
			case 1:
				ballBody.AddForce (Vector2.down * 600);
				bounce = 0;
				ballBody.velocity = new Vector2 (0.0f, 18.1f);
				clicCoeficient = 3;
				break;
			case 2:
				ballBody.AddForce (Vector2.up * bounceForce);
				bounce = 0;
				ballBody.velocity = new Vector2 (0.0f, 18.1f);
				clicCoeficient = 0.75f;
				break;
			}
		}
	}

}

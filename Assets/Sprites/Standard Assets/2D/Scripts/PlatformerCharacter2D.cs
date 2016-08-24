using System;
using UnityEngine;

namespace UnityStandardAssets._2D {
	
    public class PlatformerCharacter2D : MonoBehaviour
    {
		private CatBehaviour catBehaviour;
		public float timeLeftValue = 7f;
		public float invertedMaxSpeedDenominatorInitialValue = 5f;
		public float invertedJumpForceDenominatorInitialValue = 1.6f;
		private GameObject player;
		public float timeLeft = 0f;
		public bool inverted = false;
		public int direction = 1;
		public float invertedMaxSpeedDenominator = 1f;
		public float invertedJumpForceDenominator = 1f;
        [SerializeField] private float m_MaxSpeed = 5f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 600f;                  // Amount of force added when the player jumps.
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        public MusicController musicController;                             // Reference to the music controller to set music inversion
        public AudioSource catEffectAudioSource;                            // Audio source to play sound effects from
		public int jumpBufferFrames = 10;
		public int jumpBuffered = 0;
		public int jumpCooldownFrames = 3;
		public int jumpCooldown = 0;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private void Awake() {
            // Setting up references.
			player = GameObject.FindGameObjectWithTag ("Player");

            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
			player = GameObject.FindGameObjectWithTag("Player");
			catBehaviour = player.GetComponent<CatBehaviour> ();//TODO: player auf tag ändern
        }

        private void FixedUpdate() {
			if(inverted)
				timeLeft -= Time.deltaTime;

            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

		public void setInversion(bool isInverted) {
			inverted = isInverted;

            if (isInverted) {
				invertedMaxSpeedDenominator = invertedMaxSpeedDenominatorInitialValue;
				invertedJumpForceDenominator = invertedJumpForceDenominatorInitialValue;
				direction = -1;
			} else {
				invertedMaxSpeedDenominator = 1f;
				invertedJumpForceDenominator = 1f;
				direction = 1;
				timeLeft = timeLeftValue;
			}

            // ensure musik is (un-)inverted as well
            musicController.SetInvertedMusic(isInverted);
		}

		public bool isGrounded() {
			return m_Grounded;
		}

		public bool isFacingRight() {
			return m_FacingRight;
		}

		public void setMaxSpeed(float newValue) {
			m_MaxSpeed = newValue;
		}

		public void setJumpForce(float newValue) {
			m_JumpForce = newValue;
		}

		public void Update() {
			if (jumpBuffered > 0)
				jumpBuffered--;
			if (jumpBuffered < 0)
				jumpBuffered++;
			if (jumpCooldown > 0)
				jumpCooldown--;
		}
			
        public void Move (float move, bool jump)
		{
			//only control the player if grounded or airControl is turned on
			if (m_Grounded || m_AirControl) {
				// The Speed animator parameter is set to the absolute value of the horizontal input.
				m_Anim.SetFloat ("Speed", Mathf.Abs (move));

				// Move the character
				m_Rigidbody2D.velocity = new Vector2 (direction * move * (m_MaxSpeed / invertedMaxSpeedDenominator), m_Rigidbody2D.velocity.y);

				// If the input is moving the player right and the player is facing left...
				if (direction * move > 0 && !m_FacingRight) {
					// ... flip the player.
					Flip ();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (direction * move < 0 && m_FacingRight) {
					// ... flip the player.
					Flip ();
				}
			}

			if ((!m_Grounded || !m_Anim.GetBool ("Ground")) && jump) {
				jumpBuffered = jumpBufferFrames;
			}

			jump = (jump || (jumpBuffered > 0)) && jumpCooldown == 0;

			// If the player should jump...
			if (m_Grounded && jump && m_Anim.GetBool ("Ground")) {
				// Add a vertical force to the player.
				m_Grounded = false;
				m_Anim.SetBool ("Ground", false);
				m_Rigidbody2D.AddForce (new Vector2 (0f, m_JumpForce / invertedJumpForceDenominator));
				jumpBuffered = -(jumpBufferFrames * 2);
				jumpCooldown = jumpCooldownFrames;
			}

			if (inverted && timeLeft < 0) {
				setInversion (false);
			}
		}

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
	}
}
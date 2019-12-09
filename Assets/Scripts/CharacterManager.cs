using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    Rigidbody m_rig;
    Animator m_anim;

    bool m_isGrounded;

    [SerializeField]
    float m_massMini = 0.5f;
    [SerializeField]
    float m_timeEachDrop = 0.25f;
    [SerializeField]
    float m_massDropEachTime = 0.1f;
    [SerializeField]
    float m_weight = 1f;

    float m_time;


    private void Awake()
    {
        m_rig = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }

    void Start()
    {
    }

    int cpt = 0;
    bool hasBeenUp = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_isGrounded = false;
            m_rig.AddForce(new Vector3(0, 4, 0), ForceMode.Impulse);
        }

        if(Input.GetKey(KeyCode.Space) && !m_isGrounded && !hasBeenUp)
        {
            m_time += Time.deltaTime;
            if (m_time > m_timeEachDrop && cpt == 0)
            {
                cpt = 1;
                m_weight = m_massMini;
                m_time = 0;
            //    if (m_weight > m_massMini)
            //    {
            //        m_weight -= m_massDropEachTime;
            //    }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && !m_isGrounded) {
            hasBeenUp = true;
            m_weight = 1;
        }

    }

    private void FixedUpdate()
    {
        m_rig.AddForce(Physics.gravity * m_weight);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Ground") && m_isGrounded == false)
        {
            m_isGrounded = true;
            m_weight = 1;
            cpt = 0;
            m_time = 0;
            hasBeenUp = false;
        }
    }

    public void Move(float speed, float deltaTime)
    {
        m_anim.SetBool("is_running", true);
        transform.Translate(Vector3.forward * deltaTime * speed);
    }
}

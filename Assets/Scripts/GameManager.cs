using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private List<CharacterManager> m_characs;

    private Transform m_cam;

    [SerializeField]
    private float m_speed = 1f;

    [SerializeField]
    private Transform m_environment;
    [SerializeField]
    private GameObject m_streetPrefab;
    private List<Street> m_streets;
    private const int m_streetLength = 30;
    private const int m_maxNbStreetAtSameTime = 2;

    void Start()
    {
        m_characs = new List<CharacterManager>();
        m_characs.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterManager>());
        m_cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        m_streets = new List<Street>();
        m_streets.Add(GameObject.FindGameObjectWithTag("Street").GetComponent<Street>());
    }

    
    void Update()
    {
        m_characs[0].Move(m_speed, Time.deltaTime);
        m_cam.Translate(Vector3.right * m_speed * Time.deltaTime);

        if(m_streets[m_streets.Count - 1].MustCreateAnotherPoint(m_characs[0].transform.position))
        {
            SpawnAnotherGround();
        }
    }

    void SpawnAnotherGround()
    {
        Vector3 position = m_streets[m_streets.Count - 1].transform.position;
        position.x += m_streetLength;
        m_streets.Add(Instantiate(m_streetPrefab, position, m_streetPrefab.transform.rotation, m_environment).GetComponent<Street>());

        //-1 for the zero
        if(m_streets.Count > m_maxNbStreetAtSameTime)
        {
            var street = m_streets[0]; 
            m_streets.RemoveAt(0);
            Destroy(street.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public GameObject passenger;
    public float speed = 20f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private bool moveLeft = true;
    //private bool passengerOnBoard = false;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float move = speed * Time.fixedDeltaTime * (moveLeft? -1: 1);
        Vector3 targetVelocity = new Vector3(move * 10f, 0, 0);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        if (IsPassengerOnBoard())
        {
            // move the passenger along with the ship
            CharacterController2D passengerController = passenger.GetComponentInChildren<CharacterController2D>();
            passengerController.SetSupportVelocity(m_Rigidbody2D.velocity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("passenger on board! other=" + other + "; tag=" + other.tag);
            passenger = other.gameObject;
        }
        else if (other.CompareTag("TileMap"))
        {
            // go in opposite direction
            moveLeft = !moveLeft;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("passenger left the boat ! other=" + other + "; tag=" + other.tag);
            CharacterController2D passengerController = passenger.GetComponentInChildren<CharacterController2D>();
            passengerController.RemoveSupportVelocity();
            passenger = null;
        }
    }

    private bool IsPassengerOnBoard()
    {
        if (passenger == null) return false;
        return true;
    }

}

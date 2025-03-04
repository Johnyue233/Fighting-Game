using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Component")]
    public CharacterController PlayerControl;
    public float speed;
    public float Weight;
    //Gravity test value is -7f, will change later
    public float gravity = -7f;
    public float JumpHeight = 1f;
    public GameObject groundchecker;


    [Header("Variables")]
    public float IntInputX;
    public float IntInputY;
    public Vector3 DownVelo;
    public bool isGrounded;
    public float GroundCheckRange;
    public LayerMask GroundLayerMask;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerControl == null)
        {
            PlayerControl = gameObject.GetComponent<CharacterController>();
        }

        StartCoroutine(Gravity());
    }

    // Update is called once per frame
    void Update()
    {
        #region Jump========================
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                DownVelo.y = JumpHeight;
                Debug.Log("Jump 1");
            }
        }
        #endregion

        #region Movement========================

        IntInputX = Input.GetAxis("Horizontal");

        Vector3 Mover = transform.right * IntInputX;

        PlayerControl.Move(Mover * speed * Time.deltaTime);

        #endregion


        //IntInputY = Input.GetAxis("Vertical");
    }


    IEnumerator Gravity()
    {
        while (true)
        {
            #region Raycast variable

            RaycastHit GroundHit;
            if (Physics.Raycast(groundchecker.transform.position, Vector3.down, out GroundHit, GroundCheckRange, GroundLayerMask))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded= false;
            }
            #endregion
            if (isGrounded && !Input.GetKey(KeyCode.Space))
            {
                DownVelo.y = -1.2f;
                yield return null;
            }
            DownVelo.y += (gravity * Time.deltaTime);
            PlayerControl.Move(DownVelo * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(groundchecker.transform.position, groundchecker.transform.position + Vector3.down * GroundCheckRange);
    }
}

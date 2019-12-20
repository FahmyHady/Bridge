using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody myRB;
    Animator myAnimator;
    [SerializeField] float wavingDuration;
    [SerializeField] float characterSpeed;
    [SerializeField] float maxSpeed;
    bool running;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnimator = GetComponentInChildren<Animator>();
        StartCoroutine(EndWaveAndStartRunning());
    }
    private void FixedUpdate()
    {
        if (running)
        {
            //myRB.AddForce(transform.forward * characterSpeed);

            //if (myRB.velocity.x > maxSpeed)
            //{
            //    myRB.velocity =new Vector3(maxSpeed, myRB.velocity.y,myRB.velocity.z) ;
            //}
            myRB.velocity = new Vector3(characterSpeed, myRB.velocity.y, myRB.velocity.z); 
        }
    }
    public void DestinationReached()
    {
        running = false;
        myRB.velocity = Vector3.zero;
        StartCoroutine(TurnToFaceScreen());
    }
    IEnumerator EndWaveAndStartRunning()
    {
        yield return new WaitForSeconds(wavingDuration);
        StartCoroutine(TurnToFaceTarget());
    }
    IEnumerator TurnToFaceTarget()
    {
        while (transform.localEulerAngles.y != 90)
        {
            transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(transform.localEulerAngles.x, 90, transform.localEulerAngles.z), 60 * Time.deltaTime);
            yield return null;

        }
        myAnimator.SetTrigger("Running");
        running = true;

    }
    IEnumerator TurnToFaceScreen()
    {
        while (transform.localEulerAngles.y != 220)
        {
            transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(transform.localEulerAngles.x, 220, transform.localEulerAngles.z), 90 * Time.deltaTime);
            yield return null;
        }
        myAnimator.SetTrigger("Victory");

    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
    //private 
    private DoorScript _currentdoorscript;
    //Public

    public Transform PlayerDirection;

    public Text InfoText;

    // Use this for initialization
    void Start()
    {
        InfoText = GameObject.Find("InfoText").GetComponent<Text>();
        InfoText.gameObject.SetActive(false);
        PlayerDirection = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
        // Update is called once per frame
    void FixedUpdate () {
        RaycastHit hit;

        if (Physics.Raycast(this.PlayerDirection.position, this.PlayerDirection.forward, out hit,4))
        {
            if (hit.transform.gameObject.CompareTag("Finish"))
            {

                InfoText.gameObject.SetActive(true);
                InfoText.text = "You have won Click me to start over.";

            }
            else if (hit.transform.gameObject.CompareTag("Door"))
            {
                InfoText.gameObject.SetActive(true);
                InfoText.text = "Click To Lift Up Door.\n Door will collapse down in 5 seconds after being open";
            }
            else
            {
                InfoText.text = "";
                InfoText.gameObject.SetActive(false);
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(this.PlayerDirection.position, this.PlayerDirection.forward, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Finish"))
                {
                    SceneManager.LoadScene("Main Scene");
                }
                else if(hit.transform.gameObject.CompareTag("Door"))
                {
                    _currentdoorscript = hit.collider.gameObject.GetComponent<DoorScript>();
                    _currentdoorscript.Open();
                    StartCoroutine(CloseDoor());
                }
            }
        }

     }
    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(5);
        _currentdoorscript.Close();
    }
}

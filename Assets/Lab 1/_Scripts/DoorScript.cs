using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public Vector3 _target;
    public Vector3 DefaultPosition;
    private GameObject _door;
    private bool _opening;

	// Use this for initialization
	void Start () {
        _door = this.gameObject;
        DefaultPosition.Set(_door.transform.position.x, _door.transform.position.y, _door.transform.position.z);
        _target.Set(DefaultPosition.x, 2.86f, DefaultPosition.z);
    }
	
	// Update is called once per frame
	void Update () {
        if (_door.transform.position.y <= _target.y && _opening){
            transform.position += transform.up * Time.deltaTime;
        }
        else if(_door.transform.position.y >= DefaultPosition.y && !_opening)
        {
            transform.position -= transform.up * Time.deltaTime;
        }

    }
    public void Open()
    {
        _opening = true;
    }
    public void Close()
    {
        _opening = false;
    }

}

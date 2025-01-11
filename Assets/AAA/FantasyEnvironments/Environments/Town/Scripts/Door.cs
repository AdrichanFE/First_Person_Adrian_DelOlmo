using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	private Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}

	public void AbrirPuerta()
	{
        anim.SetTrigger("DoorOpen");
    }

	public void CerrarPuerta()
	{
        anim.SetTrigger("DoorClose");
    }

}

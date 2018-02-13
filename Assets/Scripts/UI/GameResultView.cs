using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultView : MonoBehaviour {

    [SerializeField]
    private Transform m_TargetPoint;

    [SerializeField]
    private float m_Speed;

	// Use this for initialization
	void Start( ) {
		
	}
	
	// Update is called once per frame
	void Update( ) {
        
		this.transform.position = Vector3.MoveTowards( this.transform.position, m_TargetPoint.position, Time.deltaTime * m_Speed );

        Invoke( "ReturnToTitle", 5f );
	}

    private void ReturnToTitle( ) {
        GameManager.Instance.LoadSence( "Title" );
    }
}

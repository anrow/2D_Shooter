using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Transform m_RespawnPoint;

    [SerializeField]
    private float m_RespawnDelay;

    [SerializeField]
    private bool isPause = false;

    [SerializeField]
    private PausePanelView m_PausePanel;

    [SerializeField]
    private GameResultView m_GameClearView;

    [SerializeField]
    private GameResultView m_GameOverView;

    private static GameManager instance;

    public static GameManager Instance {
        get {
            if( instance == null ) {
                instance = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<GameManager>( );
            }
            return instance;
        }
    }

    public static void DestroyPlayer( PlayerController _Player ) {
        Destroy( _Player.gameObject );
    }

    public IEnumerator RespawnPlayer( ) {
        yield return new WaitForSeconds( m_RespawnDelay );
        ObjectManager.Instance.CreateObj( ENUM_Character.Player, m_RespawnPoint.position, m_RespawnPoint.rotation );
    }

    public void GamePause( ) {
        isPause = true;
        Time.timeScale = 0;
        m_PausePanel.gameObject.SetActive( true );
    }

    public void GameResume( ) {
        isPause = false;
        Time.timeScale = 1;
        m_PausePanel.gameObject.SetActive( false );
    }

    public void GameClear( ) {
        m_GameClearView = GameObject.Find( "GameClear" ).GetComponentInChildren<GameResultView>( );
        m_GameClearView.enabled = true;
    }

    public void GameOver( ) {
        m_GameOverView = GameObject.Find( "GameOver" ).GetComponentInChildren<GameResultView>( );
        m_GameOverView.enabled = true;
    }

    public void LoadSence( string _SceneName ) {
        SceneManager.LoadScene( _SceneName );
    }

    public void QuitGame( ) {
        Application.Quit( );
    }

    private void Start( ) {
        isPause = false;
    }

    private void Update( ) {
        if( Input.GetKeyDown( KeyCode.P ) ) {
            GamePause( );
        }
    }
}

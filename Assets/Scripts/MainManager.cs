using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text MaxScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private static int m_Points;
    
    private bool m_GameOver = false;
    private int totalBricks;

    
    // Start is called before the first frame update
    void Start()
    {
        MontaTela();

        MaxScoreText.text = $"Best Score : {GameManagement.Instance.playerName} : {GameManagement.Instance.pontos}";
        ScoreText.text = $"Score : {GameManagement.Instance.nameInput} : {m_Points}";

        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        totalBricks = bricks.Length;
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Points = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if(GameManagement.Instance.pontos == 0 || GameManagement.Instance.pontos < m_Points)
        {
            GameManagement.Instance.pontos = m_Points;
            GameManagement.Instance.playerName = GameManagement.Instance.nameInput;
        }

        if(totalBricks == 0)
        {
            //MontaTela();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {GameManagement.Instance.nameInput} : {m_Points}";

        if(GameManagement.Instance.pontos > m_Points)
        {
            MaxScoreText.text = $"Best Score : {GameManagement.Instance.playerName} : {GameManagement.Instance.pontos}";
        }
        else 
        {
            MaxScoreText.text = $"Best Score : {GameManagement.Instance.nameInput} : {m_Points}";
        }

        totalBricks --;
        
    }

    public void GameOver()
    {
        GameManagement.Instance.Save();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    private void MontaTela()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }
}

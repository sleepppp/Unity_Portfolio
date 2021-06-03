using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyCore
{
    //에디터용 테스트 로더입니다. 
    public class TestLoader : MonoBehaviour
    {
        [SerializeField] string m_loadSceneName;

        private void Awake()
        {
            SceneManager.LoadSceneAsync(m_loadSceneName, LoadSceneMode.Additive);

            GameManager gameManager = GameManager.instance;     //강제 생성
        }
    }
}
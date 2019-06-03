using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Level1");
    }
}

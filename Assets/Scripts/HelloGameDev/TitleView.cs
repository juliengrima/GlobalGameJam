using UnityEngine;

namespace HelloGameDev
{
    public class TitleView : MonoBehaviour
    {
        public void StartGame()
        {
            SceneTransitionManager.LoadScene(SceneTransitionManager.Scene.Game);
        }
    }
}

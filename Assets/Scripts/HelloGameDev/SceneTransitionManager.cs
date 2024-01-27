using System.Collections;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HelloGameDev
{
    [RequireComponent(typeof(AudioSource))]
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] private Transform ScreenTransitionCanvasElement;
        [SerializeField] private SceneReference Title;
        [SerializeField] private SceneReference Game;
        [SerializeField] private SceneReference Credits;
        [SerializeField] private bool EnableOnStart;
        [SerializeField] private bool Preload;
        [SerializeField] private float PreloadDuration;
        [SerializeField] private AudioClip FadeInSound;
        [SerializeField] private AudioClip FadeOutSound;

        private static readonly int FadeInAnimatorParameter = Animator.StringToHash("FadeIn");
        private static readonly int SpeedAnimatorParameter = Animator.StringToHash("Speed");

        private static SceneTransitionManager _instance;

        private AudioSource _audioSource;
        private Animator _animator;

        private void Awake()
        {
            _instance = this;
            _audioSource = GetComponent<AudioSource>();
            _animator = ScreenTransitionCanvasElement.GetComponent<Animator>();

            ScreenTransitionCanvasElement.gameObject.SetActive(EnableOnStart);

            DontDestroyOnLoad(gameObject);
        }

        private IEnumerator Start()
        {
            if (Preload)
                yield return new WaitForSeconds(PreloadDuration);

            SceneManager.sceneLoaded += (_, _) =>
            {
                StartCoroutine(Fade(-1f, Scene.None));
            };

            yield return Fade(-1f, Scene.None);
        }

        public static void LoadScene(Scene scene)
        {
            _instance.StartCoroutine(_instance.Fade(1f, scene));
        }

        private IEnumerator Fade(float speed, Scene scene)
        {
            ScreenTransitionCanvasElement.gameObject.SetActive(true);

            _audioSource.PlayOneShot(speed > 0 ? FadeOutSound : FadeInSound);
            _animator.Play(FadeInAnimatorParameter, 0, speed > 0 ? 0f : 1f);
            _animator.SetFloat(SpeedAnimatorParameter, speed);

            var animationState = _animator.GetCurrentAnimatorStateInfo(0);
            var animationLength = animationState.length;

            yield return new WaitForSeconds(animationLength);

            if (scene != Scene.None)
                SceneManager.LoadScene(GetSceneIndex(scene).BuildIndex);
            else
                ScreenTransitionCanvasElement.gameObject.SetActive(false);
        }

        private static SceneReference GetSceneIndex(Scene scene)
        {
            return scene switch
            {
                Scene.Title => _instance.Title,
                Scene.Game => _instance.Game,
                Scene.Credits => _instance.Credits,
                _ => _instance.Title
            };
        }

        public enum Scene
        {
            None,
            Title,
            Game,
            Credits,
        }
    }
}
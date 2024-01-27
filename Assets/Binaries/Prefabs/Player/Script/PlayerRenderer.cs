using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer _renderer;

    public void SetMat(Material mat)
    {
        _renderer.material = mat;
    }
}
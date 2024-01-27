using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _renderer;

    public void SetMat(Material mat)
    {
        _renderer.material = mat;
    }
}
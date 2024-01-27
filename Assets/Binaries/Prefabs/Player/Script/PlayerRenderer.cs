using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer _renderer;

    public void SetMat(Material mat)
    {
        var mats = _renderer.materials;
        mats[1] = mat;
        _renderer.materials = mats;
    }
}
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector.Editor.Validation;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunController : MonoBehaviour
{
    [SerializeField]
    private LevelController currentLevel;
    public SceneReference sceneToLoad;

}

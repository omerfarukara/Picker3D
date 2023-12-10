using DG.Tweening;
using UnityEngine;

namespace Picker3D.Stage
{
    public class PitController : MonoBehaviour
    {
        [SerializeField] private GameObject pitObject;
        [SerializeField] private GameObject groundObject;
        
        public void MoveUp()
        {
            pitObject.transform.DOMoveY(1, 1).SetEase(Ease.OutBack).OnComplete(() =>
            {
                groundObject.SetActive(true);
            });
        }

        public void MoveDown()
        {
            pitObject.transform.DOMoveY(1, 0.1f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                groundObject.SetActive(false);
            });
        }
    }
}

using DG.Tweening;
using Picker3D.Managers;
using UnityEngine;

namespace Picker3D.Stage
{
    public class PitController : MonoBehaviour
    {
        [SerializeField] private DoorController doorController;
        [SerializeField] private GameObject pitObject;
        [SerializeField] private GameObject groundObject;
        
        public void MoveUp()
        {
            pitObject.transform.DOLocalMoveY(0, 1).SetEase(Ease.OutBack).OnComplete(() =>
            {
                groundObject.SetActive(true);
                GameManager.OnPassedStage?.Invoke();
                doorController.OpenDoor();
            });
        }

        public void MoveDown()
        {
            pitObject.transform.DOLocalMoveY(-1, 0.1f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                groundObject.SetActive(false);
            });
        }
    }
}

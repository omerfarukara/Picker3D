using DG.Tweening;
using Picker3D.Managers;
using UnityEngine;

namespace Picker3D.Stage
{
    public class PitController : MonoBehaviour
    {
        [SerializeField] private DoorController doorController;
        [SerializeField] private GameObject groundObject;
        
        public void MoveUp()
        {
            groundObject.transform.DOLocalMoveY(0, 1f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                GameManager.OnPassedStage?.Invoke();
                doorController.OpenDoor();
            });
        }

        public void MoveDown()
        {
            groundObject.transform.DOLocalMoveY(-2, 0.1f).SetEase(Ease.OutBounce);
        }
    }
}

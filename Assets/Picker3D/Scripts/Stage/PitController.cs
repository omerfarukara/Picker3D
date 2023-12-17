using DG.Tweening;
using Picker3D.LevelSystem;
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
                doorController.OpenDoor();

                if (LevelManager.Instance.levelContentData.levelObjectsData[LevelManager.Instance.Level - 1]
                        .levelStagesData.Length == LevelManager.Instance.CurrentPlayedStage)
                {
                    GameManager.OnCompleteStage?.Invoke();
                }
                else
                {
                    GameManager.OnPassedStage?.Invoke();
                }
            });
        }

        public void MoveDown()
        {
            groundObject.transform.DOLocalMoveY(-2, 0.1f).SetEase(Ease.OutBounce);
        }
    }
}
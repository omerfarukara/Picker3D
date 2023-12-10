using DG.Tweening;
using UnityEngine;

namespace Picker3D.Stage
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private GameObject rightDoor;
        [SerializeField] private GameObject leftDoor;
        
        public void OpenDoor()
        {
            rightDoor.transform.DOLocalRotate(new Vector3(0, 90, 90), 2).SetEase(Ease.InBack);
            leftDoor.transform.DOLocalRotate(new Vector3(0, 90, 90), 2).SetEase(Ease.InBack);
        }

        public void CloseDoor()
        {
            rightDoor.transform.DOLocalRotate(new Vector3(90, 90, 90), 2).SetEase(Ease.InBack);
            leftDoor.transform.DOLocalRotate(new Vector3(90, 90, 90), 2).SetEase(Ease.InBack);
        }
    }
}

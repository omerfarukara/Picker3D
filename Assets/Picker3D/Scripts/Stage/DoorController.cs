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
            rightDoor.transform.DOLocalRotate(new Vector3(165, 90, 90), 0.5f);
            leftDoor.transform.DOLocalRotate(new Vector3(15, 90, 90), 0.5f);
        }

        public void CloseDoor()
        {
            rightDoor.transform.DOLocalRotate(new Vector3(90, 90, 90), 0.5f);
            leftDoor.transform.DOLocalRotate(new Vector3(90, 90, 90), 0.5f);
        }
    }
}

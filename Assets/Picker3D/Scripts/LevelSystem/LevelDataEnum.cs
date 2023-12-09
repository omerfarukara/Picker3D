using UnityEngine;

namespace Picker3D.LevelSystem
{
    public enum StageType
    {
        None = 0,
        NormalCollectable = 1,
        BigMultiplierCollectable = 2,
        Drone = 3,
    }

    public enum CollectableType
    {
        None = 0,
        Sphere = 1,
        Cylinder = 2,
        Cube = 3,
        Triangle = 4
    }
}

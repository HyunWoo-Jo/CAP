using UnityEngine;

namespace CA.Game
{
    public interface IMover {
        public void MoveDirection(Vector3 direction);
        public bool HasReachedDesitnation();
    }
}

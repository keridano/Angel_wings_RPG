using RPG.Movement;
using UnityEngine;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour 
    {
        private void Update() 
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }   

        private void MoveToCursor()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                GetComponent<Mover>().MoveTo(hitInfo.point);
            }
        }
    }    
}


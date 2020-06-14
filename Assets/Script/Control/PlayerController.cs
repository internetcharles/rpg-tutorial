using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    void Update()
    {
        InteractWithCombat();
        InteractWithMovement();
    }

    private void InteractWithCombat()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetRay());
        foreach (var hit in hits)
        {
            CombatTarget target = hit.transform.GetComponent<CombatTarget>();
            if (target == null) continue;

            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Fighter>().Attack(target);
            }
        }
    }

    public void InteractWithMovement()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        Ray ray = GetRay();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetRay(), out hit);
        if (hasHit)
        {
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }

    private static Ray GetRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

}


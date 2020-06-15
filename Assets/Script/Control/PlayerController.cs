using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    void Update()
    {
        if (InteractWithCombat()) return;
        if (InteractWithMovement()) return;
    }

    private bool InteractWithCombat()
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
            return true;
        }
        return false;
    }

    public bool InteractWithMovement() {
    Ray ray = GetRay();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetRay(), out hit);
        if (hasHit)
        {
            if (Input.GetMouseButton(0))
            {
                GetComponent<Mover>().StartMoveAction(hit.point);
            }

            return true;
        }

        return false;
    }

    private static Ray GetRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

}


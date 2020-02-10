using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Goon : MonoBehaviour
{
    [SerializeField] float detectionRange;
    [SerializeField] GoonState state;
    Mover chaseAbility;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        chaseAbility = GetComponent<Mover>();
        EnterMode(state);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case GoonState.IDLE:

                break;
            case GoonState.ATTACK:
                break;
        }
    }

    public void EnterMode(GoonState state)
    {
        switch (state)
        {
            case GoonState.IDLE:

                break;
            case GoonState.ATTACK:
                SetTarget(FindObjectOfType<Ball>().transform);
                break;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        chaseAbility.SetTarget(target);
    }

    public enum GoonState
    {
        IDLE, ALERT, SCOUT, ATTACK, RETREAT
    }
}

using UnityEngine;
using UnityEngine.AI;

public enum AIType
{
    hide,
    pursue,
    evade
}

public class ZombieMovement : MonoBehaviour
{

    [SerializeField] GameObject _target;
    [SerializeField] AIType _type;
    
    WASDMovement pMovement;
    NavMeshAgent agent;

    Vector3 _wTarget;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pMovement = _target.GetComponent<WASDMovement>();
    }

    void Seek(Vector3 target)
    {
        agent.SetDestination(target);
    }

    void Flee(Vector3 target)
    {
        Vector3 fDir = target - transform.position;
        agent.SetDestination(transform.position - fDir);
    }

    void Chase()
    {
        Vector3 tDir = _target.transform.position - transform.position;
        float lookAhead = tDir.magnitude / (agent.speed + pMovement.currentSpeed);
        
        Vector3 Destination = _target.transform.position + _target.transform.forward * lookAhead;
        Seek(Destination);
    }
    void Evade()
    {        
        Vector3 tDir = _target.transform.position - transform.position;
        float lookAhead = tDir.magnitude / (agent.speed + pMovement.currentSpeed);
        
        Vector3 Destination = _target.transform.position + _target.transform.forward * lookAhead;
        Flee(Destination);   
    }

    void Wander()
    {
        float wRadius = 20;
        float wDistance = 10;
        float wJitter = 1;

        _wTarget += new Vector3(
                        Random.Range(-1.0f, 1.0f) * wJitter,
                        0,
                        Random.Range(-1.0f, 1.0f) * wJitter);
        
        _wTarget.Normalize();
        _wTarget *= wRadius;

        Vector3 tLocal = _wTarget + new Vector3(0, 0, wDistance);
        Vector3 tWorld = gameObject.transform.InverseTransformVector(tLocal);

        Seek(tWorld);
    }

    void Hide()
    {
        float dist = Mathf.Infinity;
        Vector3 spot = Vector3.zero;
        Vector3 cDir = Vector3.zero;
        GameObject cSpot = World.Instance.HidingSpots[0];

        var spots = World.Instance.HidingSpots;
        foreach (GameObject s in spots)
        {
            Vector3 hDir = s.transform.position - _target.transform.position;
            Vector3 hPos = s.transform.position + hDir.normalized * 5;

            float sDist = Vector3.Distance(transform.position, hPos);
            if (sDist < dist)
            {
                spot = hPos;
                cDir = hDir;
                cSpot = s;
                dist = sDist;
            }
        }
        
        Collider hCol = cSpot.GetComponent<Collider>();
        Ray back = new Ray(spot, -cDir.normalized);
        float rayDistance = 100;
        hCol.Raycast(back, out RaycastHit info, rayDistance);

        Seek(info.point + cDir.normalized * 5);
        
        /*
        if (hCol.Raycast(back, out var hit, 100f))
        {
            Vector3 destination = hit.point + hit.normal * 5f;
            Seek(destination);
        }
        */
        
    }

    bool HasSight()
    {
        Vector3 toTarget = _target.transform.position - transform.position;
        float distance = toTarget.magnitude;
        if (Physics.Raycast(transform.position, toTarget.normalized, out RaycastHit info, distance))
        {
            return info.transform.CompareTag("Player");
            
        }
        return false;
    }

    void Update()
    {
        float distance = 10;
        if (_type == AIType.hide)
        {
            if (DistanceToTarget() > distance)
                Wander();
            else
            {
            if (HasSight())
                Hide();
            }
        }

        if (_type == AIType.pursue)
        {
            if (DistanceToTarget() > distance)
                Wander();
            else
                Chase();
        }

        if (_type == AIType.evade)
        {
            if (DistanceToTarget() > distance)
                Wander();
            else
                Evade();
        }
    }

    float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, _target.transform.position);
    }
}

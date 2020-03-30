using UnityEngine;

public class Disparo : MonoBehaviour
{
    public HandlePortals portalManager;
    private RaycastHit hitGun;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Inicio");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitGun, Mathf.Infinity) 
                && hitGun.transform.tag != "Portal")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitGun.distance, Color.yellow);
                Debug.DrawRay(hitGun.point, hitGun.normal * hitGun.distance, Color.blue);
                //portalManager.showPortalOnLocation(true, hitGun.point, hitGun.normal);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitGun, Mathf.Infinity)
                && hitGun.transform.tag != "Portal")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitGun.distance, Color.yellow);
                Debug.DrawRay(hitGun.point, hitGun.normal * hitGun.distance, Color.blue);
                //portalManager.showPortalOnLocation(false, hitGun.point, hitGun.normal);
            }
        }
    }
}

using UnityEngine;

public class HandlePortals : MonoBehaviour
{
    public GameObject bluePortal;
    public GameObject redPortal;
    public GameObject bluePortalCam;
    public GameObject redPortalCam;
    private Vector3 originalScale;
    
    // Start is called before the first frame update
    void Start()
    {
        originalScale = bluePortal.transform.localScale;
        sendPortalToInsideCharacter(bluePortal);
        sendPortalToInsideCharacter(redPortal);
    }

    // Update is called once per frame
    void Update()
    {
        if (redPortal.activeInHierarchy && bluePortal.activeInHierarchy)
        {
            redPortalCam.transform.LookAt(transform.position * -1);
            //redPortal.transform.forward = redPortal.transform.forward * -1;
            bluePortalCam.transform.LookAt(transform.position * -1);
            //bluePortal.transform.forward = bluePortal.transform.forward * -1;
        }
    }

    public void sendPortalToInsideCharacter(GameObject portal)
    {
        portal.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        portal.transform.position = gameObject.transform.position;
        portal.SetActive(false);
    }

    public void showPortalOnLocation(bool portal, Vector3 hitPoint, Vector3 newForward)
    {
        Vector3 targetPos = hitPoint + (newForward.normalized * 0.01f);
        if (portal) {
            moveBluePortal(targetPos, newForward);
        }
        else { 
            moveRedPortal(targetPos, newForward);
        }
    }

    private void moveBluePortal(Vector3 newPos, Vector3 newForw)
    {
        bluePortal.transform.forward = Vector3.up;
        //bluePortal.transform.up = newForw;
        bluePortal.transform.position = newPos;
        bluePortal.transform.localScale = originalScale;
        bluePortal.SetActive(true);
    }

    private void moveRedPortal(Vector3 newPos, Vector3 newForw)
    {
        redPortal.transform.forward = Vector3.up;
        //redPortal.transform.up = newForw;
        redPortal.transform.position = newPos;
        redPortal.transform.localScale = originalScale;
        redPortal.SetActive(true);
    }
}

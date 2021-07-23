using UnityEngine;

enum MotionModel { OnPlane, InAir }
public class RaycastController : MonoBehaviour
{
    private new UnityEngine.Camera camera = null;
    private Vector2 screenCenter;

    // Start is called before the first frame update
    private void Awake()
    {
        camera = UnityEngine.Camera.main;
    }

    private bool CustomRaycast(Vector2 targetFrom)
    {
        if (Physics.Raycast(camera.ScreenPointToRay(targetFrom), out RaycastHit raycastHit))
        {
            GameObject selectedObject = raycastHit.collider.gameObject;

            if (selectedObject.tag == "Destroyable")
            {
                LifecycleController lifeCycleC = selectedObject.GetComponent<LifecycleController>();
                lifeCycleC?.OnKillObject();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RaycastFromMousePosition()
    {
        return CustomRaycast(Input.mousePosition);
    }

    public void RaycastFromCenter()
    {
        CustomRaycast(new Vector2(Screen.width / 2f, Screen.height / 2f));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastFromMousePosition();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{   
    public static bool inInteractionMode = false;
    public GameObject Gernade_pointer; // 声明 'GernadePointer' 变量
    public GameObject Pistol; // 声明 'Pistol' 变量
    public Transform player; // 对玩家或相机的引用
    public float distanceFromPlayer = 5f; // 与玩家的期望距离
    private bool continueFunction = false;
    public Camera Main_Camera; // Assign the main camera in the Inspector
    public GameObject originObject; // Assign the origin GameObject in the Inspector
    public Vector3 direction = Vector3.forward; // Direction of the ray
    public float rayLength = 10f; // Length of the ray
    private GameObject selectedObject;
    public static bool shootable = true;
    // public Text modeswitching;
    private Target_Grab specialScript; // 定义一个私有的SpecialScript类型的变量

    // GameObject objCheck;
    // private string words_grab = "You can grab stuff now";
    // private string words_shoot = "You can shoot enemies now";
    void Start(){
        specialScript = GetComponent<Target_Grab>();
        Gernade_pointer.SetActive(false);
        Pistol.SetActive(false);
    }

    void Update()
    {           
        if (Input.GetKeyDown(KeyCode.F))
        {   
            Gernade_pointer.SetActive(true);
            Pistol.SetActive(false);
            continueFunction = true;
            shootable = false;
            // modeswitching.text = words_grab.ToString();
        }

        if (continueFunction)
        {
            if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
                    {   
                        inInteractionMode = false;
                    }
            if (Input.GetMouseButtonDown(1)) // 0 represents the left mouse button
                    {   
                        inInteractionMode = true;
                    }
            if (Main_Camera != null && originObject != null)
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray myRay = Main_Camera.ScreenPointToRay(mousePosition);
                RaycastHit raycastHit;
                specialScript = GetComponent<Target_Grab>();

                if (Physics.Raycast(myRay, out raycastHit) && raycastHit.distance <= 5)
                {   

                    // Check if the "Target_Grab" tag exists
                    //                     IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();

                    // if (raycastHit.collider != null && tg_interactable == true)
                    // {
                    //     // The object is interactable (contains the script implementing IInteractable)
                    //     Debug.Log("Object is interactable!");
                    // }
                    // else
                    // {
                    //     // The object is not interactable (does not contain the script implementing IInteractable)
                    //     Debug.Log("Object is not interactable!");
                    // }
                    
                    // Access information about the hit object
                    GameObject hitObject = raycastHit.collider ? raycastHit.collider.gameObject : null;
                    Transform hitTransform = raycastHit.transform;
                    Vector3 hitPoint = raycastHit.point;
                    float hitDistance = raycastHit.distance;
                    specialScript = hitObject.GetComponent<Target_Grab>(); // 获取击中物体上的Target_Grab脚本
                    // Example: Change the hit object's color to red
                    Renderer hitRenderer = hitObject ? hitObject.GetComponent<Renderer>() : null;
                    // if (hitRenderer != null)
                    // {
                    //     // hitRenderer.material.color = Color.red;
                    // }

                    // Draw debug lines
                    // Debug.DrawLine(myRay.origin, hitPoint, Color.blue);
                    // Debug.DrawRay(hitPoint, raycastHit.normal * 2f, Color.yellow);

                    // Log information about the hit object
                    // Debug.Log("Hit Object: " + hitObject.name);
                    // Debug.Log("Hit Point: " + hitPoint);
                    // Debug.Log("Hit Distance: " + hitDistance);

                    // Check if left mouse button is clicked
                    if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
                    {   
                        selectedObject = hitObject;
                        // Show information about the hit object
                        // Debug.Log("Clicked Left Mouse Button! Object: " + hitObject.name);
                        // Debug.Log("Hit Point: " + hitPoint);
                        // Debug.Log("Hit Distance: " + hitDistance);
                        // Debug.Log("Script"+specialScript);
                    }
                    if (selectedObject != null && Input.GetMouseButton(0) && specialScript != GetComponent<Target_Grab>())
                    {   
                        // Calculate mouse movement delta
                        float deltaX = Input.GetAxis("Mouse X");
                        float deltaY = Input.GetAxis("Mouse Y");

                        // Move the selected object using mouse movement
                        Vector3 newPosition = selectedObject.transform.position + new Vector3(deltaX, deltaY, 0f);
                        selectedObject.transform.position = newPosition;
                    }
                    if (selectedObject != null && Input.GetMouseButton(1) && specialScript != GetComponent<Target_Grab>())
                    {   
                        if (player != null && inInteractionMode == true)
                        {
                            // 获取玩家的朝向向量，并将其转换为世界坐标系下的方向
                            Vector3 playerForward = player.forward.normalized;

                            // 计算所需位置，基于玩家位置和朝向向量
                            Vector3 desiredPosition = player.position + playerForward * distanceFromPlayer;

                            // 更新所选物体的位置到所需位置，但保持其Y坐标不变
                            selectedObject.transform.position = new Vector3(desiredPosition.x, selectedObject.transform.position.y, desiredPosition.z);
                        }
                    }

                }
                else
                {
                    // specialScript = GetComponent<Target_Grab>();
                    // Debug.Log("Not Interactable");
                    // Debug.Log("Scripts:" + specialScript);
                }

                Debug.DrawRay(myRay.origin, myRay.direction * rayLength, Color.green);
            }
            else
            {
                // specialScript = GetComponent<Target_Grab>();
                // Debug.Log("Not Interactable");
                // Debug.Log("Scripts:" + specialScript);

            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {   
            Gernade_pointer.SetActive(false);
            Pistol.SetActive(true);
            continueFunction = false;
            shootable = true;
            // modeswitching.text = words_shoot.ToString();

        }
    }

    // Method to check if a tag is defined in Unity
    bool IsTagDefined(string tag)
    {
        return UnityEditorInternal.InternalEditorUtility.tags.Contains(tag);
    }
    void FixedUpdate()
    {
        if (selectedObject != null && Input.GetMouseButton(1) && specialScript != GetComponent<Target_Grab>())
        {
            if (player != null && inInteractionMode == true)
            {
                // 获取玩家的朝向向量，并将其转换为世界坐标系下的方向
                Vector3 playerForward = player.forward.normalized;

                // 计算所需位置，基于玩家位置和朝向向量
                Vector3 desiredPosition = player.position + playerForward * distanceFromPlayer;

                // 尝试移动所选物体到所需位置，但保持其Y坐标不变
                Rigidbody selectedRigidbody = selectedObject.GetComponent<Rigidbody>();
                Vector3 newPosition = new Vector3(desiredPosition.x, selectedObject.transform.position.y, desiredPosition.z);
                selectedRigidbody.MovePosition(newPosition);

                // 检测是否发生碰撞
                Collider[] colliders = Physics.OverlapBox(selectedObject.transform.position, selectedObject.transform.localScale / 2f);
                foreach (Collider collider in colliders)
                {
                    if (collider != selectedObject.GetComponent<Collider>())
                    {
                        // 碰撞发生，将位置还原到上一帧位置
                        selectedRigidbody.MovePosition(selectedRigidbody.position - selectedRigidbody.velocity * Time.fixedDeltaTime);
                        break;
                    }
                }
            }
        }
    }

}

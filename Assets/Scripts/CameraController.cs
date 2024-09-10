using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private PlayerController player;
    [SerializeField] private Vector3 offset;
    public static bool isAddFOV;
    private readonly static float addCameraFOV = 7;
    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isAddFOV = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAddFOV)
        {
            player.canControl = false;
            StartCoroutine(AddFOV(GetComponent<Camera>().fieldOfView + addCameraFOV));
            isAddFOV = false;
        }


    }
    private void LateUpdate()
    {
        if (!GameManager.isPaused)
        {
            if (player != null)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.circleParent.transform.position.x, 0, player.circleParent.transform.position.z) + offset, speed * Time.deltaTime);    
                transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
    IEnumerator AddFOV(float fov)
    {
        float t = 0;
        while (GetComponent<Camera>().fieldOfView < fov)
        {
            yield return new WaitForSeconds(.1f);
            t += 2 * Time.fixedDeltaTime;
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, fov, t);
        }
        player.canControl = true;
    }
}

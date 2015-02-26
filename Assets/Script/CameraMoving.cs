using UnityEngine;
using System.Collections;
//cleanLevel = 0
public class CameraMoving : MonoBehaviour
{

    private Transform hitTransform;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? 20f : 5f;
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float y = 0;

        transform.Translate(x, y, z);

    }

    bool flag = false;

    void OnGUI()
    {
        if (Input.GetMouseButtonDown(1))
        {
            flag = true;
            Debug.Log("Right Down");
        }
        else if (Input.GetMouseButtonUp(1))
        {
            flag = false;
            Debug.Log("Right Up");
        }
        if (flag)
        {
            Vector3 axis = new Vector3(-1 * cosRadian(transform.eulerAngles.y), 0, sinRadian(transform.eulerAngles.y));

            float speed = Input.GetKey(KeyCode.LeftShift) ? 150f : 33f;
            float vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * 33f;

            float index = ((1 / cosRadian(transform.eulerAngles.x)) * (1 / cosRadian(transform.eulerAngles.x))) % 3;
            float horizon = Input.GetAxis("Mouse X") * Time.deltaTime * 33f * index;

            transform.RotateAround(transform.localPosition, Vector3.up, horizon * Time.deltaTime * speed);
            transform.RotateAround(transform.localPosition, axis, vertical * Time.deltaTime * speed);
        }
    }

    float toRadian(float degree)
    {
        return Mathf.PI * degree / 180;
    }

    float sinRadian(float degree)
    {
        return Mathf.Sin(toRadian(degree));
    }

    float cosRadian(float degree)
    {
        return Mathf.Cos(toRadian(degree));
    }

}

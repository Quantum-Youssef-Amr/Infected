using UnityEngine;

public class firingbutton : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 0){
            transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}

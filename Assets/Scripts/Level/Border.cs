using UnityEngine;

public class Border : MonoBehaviour
{ 
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            EventManager.LineCrossed();
        }
    }
}
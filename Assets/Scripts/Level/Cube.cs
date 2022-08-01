using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    public int value = 2;
    public Text[] edgesText = new Text[6];

    private float jumpPower = 4f;
    private Color color;
    private Material material;
    private Rigidbody cubeRigidbody;

    void Awake()
    {
        material = gameObject.GetComponent<Renderer>().material;
        cubeRigidbody = gameObject.GetComponent<Rigidbody>();

    }

    void OnCollisionStay(Collision collision)
    {
        DetectCollision(collision);
    }

    void DetectCollision(Collision collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            Cube collisionCube = collision.gameObject.GetComponent<Cube>();
            if (collisionCube.value == this.value)
            {
                Destroy(collision.gameObject);
                RaiseValue();
                UpdateVisual();
                Jump();
                EventManager.CubeMerged(value);
            }
        }
    }

    void RaiseValue()
    {
        value *= 2;
    }

    public void ChangeValue(int newValue)
    {
        value = newValue;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        float valuePower = Mathf.Log(value, 2);
        int index = (int)valuePower;
        if (index <= LevelManager.currentPalette.palette.Length)
        {
            string colorHex = LevelManager.currentPalette.palette[index - 1];
            if (ColorUtility.TryParseHtmlString(colorHex, out color))
            {
                material.color = color;
            }
        }
        else
        {
            material.color = Color.black;
        }

        foreach (Text text in edgesText)
        {
            text.text = value.ToString();
        }
    }

    void Jump()
    {
        cubeRigidbody.AddForce(Vector3.up * jumpPower + Vector3.forward, ForceMode.Impulse);
    }
}
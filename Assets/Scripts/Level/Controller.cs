using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject[] bonusCubes;

    [SerializeField] Vector3 startPositon = new Vector3(0, 0, 0);

    private Touch touch;

    private float pushPower = 12f;
    private GameObject selectedCube = null;
    private Rigidbody selectedCubeRb;
    private StandardCube cube;

    private bool onUi = false;
    private float stationaryTouchX;
    private float movingLimit = 1.8f;
    private float movingCoeff = 0.15f;

    void Start()
    {
        ChooseStandrdCube();
    }

    void Update()
    {

        if(Input.touchCount > 0)
        {

            touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
            {
                stationaryTouchX = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Moved && selectedCube != null)
            {
                MoveCube();
            }
            else if (touch.phase == TouchPhase.Ended && selectedCube != null)
            {
                if (!onUi)
                {
                    PushCube();
                    onUi = false;
                }
                else if (onUi)
                {
                    onUi = false;
                }
            }
        }
    }

    void GenerateCube(GameObject cube)
    {
        selectedCube = Instantiate(cube, startPositon, cubePrefab.transform.rotation);
        selectedCubeRb = selectedCube.GetComponent<Rigidbody>();
    }

    void MoveCube()
    {
        float moveTouchX = touch.position.x;
        float direction = 0f;

        if (stationaryTouchX != moveTouchX)
        {
            direction = (stationaryTouchX - moveTouchX) / Mathf.Abs(stationaryTouchX - moveTouchX);
        }

        float x = Mathf.Clamp(selectedCube.transform.position.x - direction * movingCoeff, -movingLimit, movingLimit);
        selectedCube.transform.position = new Vector3(x, selectedCube.transform.position.y, selectedCube.transform.position.z);
    }

    void PushCube()
    {
        startPositon = selectedCube.transform.position;
        selectedCubeRb.constraints = RigidbodyConstraints.None;
        selectedCubeRb.AddForce(Vector3.forward * pushPower, ForceMode.VelocityChange);
        StartCoroutine(DelayGeneration(selectedCube));
        selectedCube = null;

        EventManager.CubePushed();
    }

    void ChooseStandrdCube()
    {
        GenerateCube(cubePrefab);
        cube = selectedCube.GetComponent<StandardCube>();
        int randomIndex = Random.Range(0, LevelManager.availableValues.Count);
        cube.ChangeValue(LevelManager.availableValues[randomIndex]);
    }

    public void ChooseBonusCube(int index)
    {
        onUi = true;

        if (selectedCube != null)
        {
            Destroy(selectedCube);
            selectedCube = null;
            GameObject bonusCubePrefab = bonusCubes[index];
            GenerateCube(bonusCubePrefab);
        }
    }

    private IEnumerator DelayGeneration(GameObject lastSelectedCube)
    {
        yield return new WaitForSeconds(1);
        if (lastSelectedCube)
        {
            lastSelectedCube.tag = "Cube";
        }
        ChooseStandrdCube();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(startPositon, Vector3.one);
    }
}
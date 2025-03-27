using UnityEngine;
using UnityEngine.UIElements;

public class EnterExitCar : MonoBehaviour
{
    public GameObject player;
    public GameObject car;
    public KeyCode enterExitKey = KeyCode.E;
    public Transform carDriverSeat;
    public Collider carCollider;
    public Collider playerCollider;

    public static bool isInCar = false;
    MoveCamera moveCam = new MoveCamera();
    PlayerCam playerCam = new PlayerCam();

    void Update()
    {
        if (Input.GetKeyDown(enterExitKey))
        {
            if (isInCar)
            {
                ExitCar();
            }
            else if (Vector3.Distance(player.transform.position, car.transform.position) < 3.0f)
            {
                EnterCar();
            }
            moveCam.Update();
            playerCam.Start();
            playerCam.Update();
        }
    }

    void EnterCar()
    {
        isInCar = true;
        player.SetActive(false);
        player.transform.position = carDriverSeat.position;
        player.transform.parent = carDriverSeat;
        car.GetComponent<CarController>().enabled = true;
    }

    void ExitCar()
    {
        isInCar = false;
        player.SetActive(true);
        player.transform.parent = null;
        car.GetComponent<CarController>().enabled = false;
    }
}
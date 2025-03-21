using UnityEngine;

public class EnterExitCar : MonoBehaviour
{
    public GameObject player;
    public GameObject car;
    public KeyCode enterExitKey = KeyCode.E;
    public Transform carDriverSeat;
    public Collider carCollider;
    public Collider playerCollider;

    private bool isInCar = false;

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
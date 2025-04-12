using UnityEngine;

public class CarFollowCamera : MonoBehaviour
{
    [Header("Alap Beállítások")]
    public Transform car;  // Húzd ide az autó transformját az Inspectorban
    public float distance = 5f;  // Kamera távolsága az autótól
    public float height = 2f;  // Kamera magassága
    public float smoothSpeed = 5f;  // Mozgás simasága (nagyobb érték = gyorsabb követés)

    void LateUpdate()
    {
        // 1. Számold ki a kívánt pozíciót (autó mögött, kissé feljebb)
        Vector3 wantedPosition = car.position - car.forward * distance + Vector3.up * height;
        
        // 2. Mozgasd a kamerát simán a kívánt pozícióba
        transform.position = Vector3.Lerp(transform.position, wantedPosition, smoothSpeed * Time.deltaTime);
        
        // 3. Nézzen a kamera az autóra
        transform.LookAt(car);
    }
}
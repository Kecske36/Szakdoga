using UnityEngine;
using UnityEngine.UI;

public class SpeedDisplay : MonoBehaviour
{
    public Rigidbody targetRigidbody;  // Az objektum, aminek a sebességét követjük
    public Text speedText;             // A Text UI elem
    public string unit = " km/h";      // Mértékegység (opcionális)
    public int decimalPlaces = 1;      // Tizedesjegyek száma

    void Update()
    {
        if (targetRigidbody != null && speedText != null)
        {
            // Sebesség kiszámolása (magnitude = vektor hossza)
            float speed = targetRigidbody.velocity.magnitude;
            
            // Formázott kiírás
            speedText.text = "Sebesség: " + speed.ToString("F" + decimalPlaces) + unit;
            
            // Vagy alternatív megoldás String.Format használatával:
            // speedText.text = string.Format("Sebesség: {0:F1}{1}", speed, unit);
        }
    }
}
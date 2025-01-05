using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public GameObject h1;
    public GameObject h2;
    public GameObject h3;
    public GameObject h4;
    public GameObject h5;

    public TMP_Text text;

    public Movement player;

    private void Update()
    {
        if (player.health == 5)
        {
            h1.SetActive(true);
            h2.SetActive(true);
            h3.SetActive(true);
            h4.SetActive(true);
            h5.SetActive(true);
        }
        else if (player.health == 4)
        {
            h1.SetActive(true);
            h2.SetActive(true);
            h3.SetActive(true);
            h4.SetActive(true);
            h5.SetActive(false);
        }
        else if (player.health == 3)
        {
            h1.SetActive(true);
            h2.SetActive(true);
            h3.SetActive(true);
            h4.SetActive(false);
            h5.SetActive(false);
        }
        else if (player.health == 2)
        {
            h1.SetActive(true);
            h2.SetActive(true);
            h3.SetActive(false);
            h4.SetActive(false);
            h5.SetActive(false);
        }
        else if (player.health == 1)
        {
            h1.SetActive(true);
            h2.SetActive(false);
            h3.SetActive(false);
            h4.SetActive(false);
            h5.SetActive(false);
        }
        else if (player.health == 0)
        {
            h1.SetActive(false);
            h2.SetActive(false);
            h3.SetActive(false);
            h4.SetActive(false);
            h5.SetActive(false);
        }
        text.text = player.health.ToString();
    }

}

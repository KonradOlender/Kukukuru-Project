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
            if(player.HealthBarReset == true)
            {
                player.HealthBarReset = false;
                h1.GetComponent<Hearts>().ResetSprites();
                h2.GetComponent<Hearts>().ResetSprites();
                h3.GetComponent<Hearts>().ResetSprites();
                h4.GetComponent<Hearts>().ResetSprites();
                h5.GetComponent<Hearts>().ResetSprites();
            }
            
        }
        else if (player.health == 4)
        {
            if (player.HealthBarReset == true)
            {
                player.HealthBarReset = false;
                h1.GetComponent<Hearts>().ResetSprites();
                h2.GetComponent<Hearts>().ResetSprites();
                h3.GetComponent<Hearts>().ResetSprites();
                h4.GetComponent<Hearts>().ResetSprites();
                h5.GetComponent<Hearts>().TriggerAnim();
            }
        }
        else if (player.health == 3)
        {
            if (player.HealthBarReset == true)
            {
                player.HealthBarReset = false;
                h1.GetComponent<Hearts>().ResetSprites();
                h2.GetComponent<Hearts>().ResetSprites();
                h3.GetComponent<Hearts>().ResetSprites();
                h4.GetComponent<Hearts>().TriggerAnim();
            }
        }
        else if (player.health == 2)
        {
            if (player.HealthBarReset == true)
            {
                player.HealthBarReset = false;
                h1.GetComponent<Hearts>().ResetSprites();
                h2.GetComponent<Hearts>().ResetSprites();
                h3.GetComponent<Hearts>().TriggerAnim();
            }
        }
        else if (player.health == 1)
        {
            if (player.HealthBarReset == true)
            {
                player.HealthBarReset = false;
                h1.GetComponent<Hearts>().ResetSprites();
                h2.GetComponent<Hearts>().TriggerAnim();
            }
        }
        else if (player.health == 0)
        {
            if (player.HealthBarReset == true)
            {
                player.HealthBarReset = false;
                h1.SetActive(false);
                h2.SetActive(false);
                h3.SetActive(false);
                h4.SetActive(false);
                h5.SetActive(false);
            }
        }
        text.text = player.health.ToString();
    }

}

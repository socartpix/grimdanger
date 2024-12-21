using UnityEngine;

public class CheckGems : MonoBehaviour 
{
    public GameObject[] Gems_blue, Gems_red, Gems_yellow;

    void Start()
    {
        Debug.Log(GameManager.Gems_blue );
        if(GameManager.Instance != null)
        {
            // Activa/Desactiva gemas azules
            foreach(GameObject gem in Gems_blue)
            {
                gem.SetActive(GameManager.Gems_blue > 0);
            }

            // Activa/Desactiva gemas rojas
            foreach(GameObject gem in Gems_red)
            {
                gem.SetActive(GameManager.Gems_red > 0);
            }

            // Activa/Desactiva gemas amarillas
            foreach(GameObject gem in Gems_yellow)
            {
                gem.SetActive(GameManager.Gems_yellow > 0);
            }
        }
    }

    // También podrías querer actualizar esto cuando cambien los contadores
    public void UpdateGemsVisibility()
    {
        if(GameManager.Instance != null)
        {
            foreach(GameObject gem in Gems_blue)
            {
                gem.SetActive(GameManager.Gems_blue > 0);
            }

            foreach(GameObject gem in Gems_red)
            {
                gem.SetActive(GameManager.Gems_red > 0);
            }

            foreach(GameObject gem in Gems_yellow)
            {
                gem.SetActive(GameManager.Gems_yellow > 0);
            }
        }
    }
}
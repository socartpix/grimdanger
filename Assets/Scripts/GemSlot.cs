using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemSlot : MonoBehaviour
{
    private Image currentGem = null;
    private string currentGemTag = "";
    private int gemCount = 0;
    public Image resultSlot; // Arrastra aquí el slot donde aparecerá el resultado

    public bool CanAcceptGem()
    {
        return gemCount < 2;
    }

    public void AddGem(GameObject gem)
    {
        if (gemCount == 0)
        {
            currentGemTag = gem.tag;
            gemCount++;
            Debug.Log("Primera gema añadida: " + currentGemTag);
        }
        else if (gemCount == 1 && gem.CompareTag(currentGemTag))
        {
            gemCount++;
            Debug.Log("Segunda gema añadida, iniciando combinación");
            CheckCombination();
        }
        else
        {
            Debug.Log("No se puede añadir gema diferente");
        }
    }

    private void CheckCombination()
    {
        if (gemCount == 2)
        {
            CreateCombinationResult();
            gemCount = 0;
            currentGemTag = "";
        }
    }

    private void CreateCombinationResult()
    {
        if (resultSlot != null)
        {
            resultSlot.gameObject.SetActive(true);

            switch (currentGemTag)
            {
                case "Gem_Blue":
                    // Aquí asignarías el sprite de la gema azul al resultSlot
                    Debug.Log("¡Combinación exitosa! Nueva gema azul creada");
                    break;
                case "Gem_Red":
                    // Aquí asignarías el sprite de la gema roja al resultSlot
                    Debug.Log("¡Combinación exitosa! Nueva gema roja creada");
                    break;
                case "Gem_Yellow":
                    // Aquí asignarías el sprite de la gema amarilla al resultSlot
                    Debug.Log("¡Combinación exitosa! Nueva gema amarilla creada");
                    break;
            }
        }
    }
}
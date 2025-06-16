using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CardSelector : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<XRBaseInteractable>().selectEntered.AddListener(OnSelect);
    }

    private void OnDisable()
    {
        GetComponent<XRBaseInteractable>().selectEntered.RemoveListener(OnSelect);
    }

    public void OnSelect(SelectEnterEventArgs args)
    {
        CardSelected.Instance.ToggleCardSelection(gameObject);
    }
}
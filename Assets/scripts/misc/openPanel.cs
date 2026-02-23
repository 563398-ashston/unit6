using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject uiPanel;

    void Start()
    {
        uiPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UIPanelTrigger"))
        {
            uiPanel.SetActive(true);
        }
    }
     /*
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UIPanelTrigger"))
        {
            uiPanel.SetActive(false);
        }
    }
     */
}
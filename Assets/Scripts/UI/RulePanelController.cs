using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RulePanelController : MonoBehaviour
{

    [Header("Pages")]
    public GameObject panelPage1;
    public GameObject panelPage2;
    [Space]

    [Header("Buttons")]
    public Button arrowButtonRight;
    public Button arrowButtonLeft;
    public Button closeButton;
    [Space]

    [Header("Countdown details")]
    public TMP_Text countdownText;

    private bool onFirstPage = true;

    void Start()
    {
        if (gameObject == null)
            return;

        panelPage1.SetActive(true);
        panelPage2.SetActive(false);

        arrowButtonRight.onClick.AddListener(FlipPage1To2);

        arrowButtonLeft.onClick.AddListener(FlipPage2To1);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseButtonClicked);
    }


    void FlipPage1To2() // Changes panel on button press (page 1 to 2)
    {
        onFirstPage = false;

        panelPage1.SetActive(false);
        panelPage2.SetActive(true);
    }

    void FlipPage2To1() // Changes panel on button press (page 2 to 1)
    {
        onFirstPage = true;

        panelPage1.SetActive(true);
        panelPage2.SetActive(false);
    }

    void OnCloseButtonClicked()
    {
        gameObject.SetActive(false); // Hide panel

        // Notify UIManager that panel closed
        UIManager.Instance.OnRulesPanelClosed();
    }
}

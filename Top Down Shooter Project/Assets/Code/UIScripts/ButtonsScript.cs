using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class ButtonsScript : MonoBehaviour
{
    [Header("alle elementjes")]
    public GameObject Controls;
    public GameObject ControlsShadow;
    public TextMeshProUGUI ControlText;
    public Light2D ControlSpot;
    public Light2D ExtraSpotlight;

    [Header("Instellingen voor glitches")]
    public float minSchudTijd = 0.2f;
    public float maxSchudTijd = 1f;
    public float textSchudSterkte = 5f;
    public float minExtraFlits = 0.5f;
    public float maxExtraFlits = 1.5f;
    public float extraGlitchInterval = 0.1f;

    private Vector3 textStartPos;
    private Coroutine textGlitchCoroutine;

    void Start()
    {
        // Zeker maken dat ALLES goed is aan/uit + de positie en starten van coroutine van de glitch op de spotlight en tekst!
        if (ControlText != null)
            textStartPos = ControlText.transform.localPosition;
        if (Controls != null)
            Controls.SetActive(false);
        if (ControlsShadow != null)
            ControlsShadow.SetActive(false);
        if (ExtraSpotlight != null)
            ExtraSpotlight.enabled = true;
            StartCoroutine(ExtraSpotlightGlitch());
        if (ControlSpot != null)
            ControlSpot.enabled = false;
    }

    // De click functie voor bij de controlsbut
    public void ToggleControls()
    {
        if (Controls == null) return;

        bool isActief = !Controls.activeSelf;
        Controls.SetActive(isActief);

        if (ControlSpot != null)
            ControlSpot.enabled = isActief;
        if (ControlsShadow != null)
            ControlsShadow.SetActive(isActief);

        if (textGlitchCoroutine != null)
            StopCoroutine(textGlitchCoroutine);

        if (isActief && ControlText != null)
            textGlitchCoroutine = StartCoroutine(TextGlitch());
        else if (ControlText != null)
            ControlText.transform.localPosition = textStartPos;
    }

    // Coroutine voor tekst glitch
    IEnumerator TextGlitch()
    {
        while (Controls.activeSelf)
        {
            float x = Random.Range(-1f, 1f) * textSchudSterkte;
            float y = Random.Range(-1f, 1f) * textSchudSterkte;
            ControlText.transform.localPosition = textStartPos + new Vector3(x, y, 0);

            float waitTime = Random.Range(minSchudTijd, maxSchudTijd);
            yield return new WaitForSeconds(waitTime);

            ControlText.transform.localPosition = textStartPos;
        }

        ControlText.transform.localPosition = textStartPos;
    }

    // Coroutine voor glitch van xtra spotlight
    IEnumerator ExtraSpotlightGlitch()
    {
        while (true)
        {
            if (ExtraSpotlight != null)
                ExtraSpotlight.intensity = Random.Range(minExtraFlits, maxExtraFlits);

            yield return new WaitForSeconds(extraGlitchInterval);
        }
    }

    // Start 
    public void StartGame()
    {
        SceneManager.LoadScene("Scene 1");
    }

    // quit
    public void Quit()
    {
        Application.Quit();
    }
}
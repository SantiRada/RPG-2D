using UnityEngine;
using UnityEngine.UI;

public class ResolutionSize : MonoBehaviour {

    [Header("Data")]
    [HideInInspector] public int id;
    [HideInInspector] public int ancho;
    [HideInInspector] public int alto;

    [Header("Object")]
    private GameObject bg;
    private Mask viewport;

    [Header("Calls")]
    private ConfigManager config;

    private void Start()
    {
        viewport = GetComponentInParent<Mask>();
        config = FindObjectOfType<ConfigManager>();
    }
    public void ChangeResolution()
    {
        bg = GameObject.Find("BackgroundChangeResolution");
        if(bg != null)
            bg.SetActive(false);

        viewport.enabled = true;
        config.ChangeResolution(id, ancho, alto);
    }
}

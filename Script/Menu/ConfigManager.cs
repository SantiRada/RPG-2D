using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private bool sumOffset;
    [SerializeField] private float offsetSize;
    [SerializeField] private int sizeResolutionCube;
    [SerializeField] private Color colorSelector;
    private FullScreenMode completeScreen;
    private bool approve;

    [Header("Data")]
    private int ancho;
    private int alto;
    private int lastAncho;
    private int lastAlto;
    private int currentID;
    private int id;

    [Header("Object")]
    [SerializeField] private GameObject approveZone;
    [SerializeField] private Text timerToApprove;
    [Space]
    [SerializeField] private GameObject parentResolution;
    [SerializeField] private GameObject resolutionPrefab;
    private List<GameObject> resolutionList = new List<GameObject>();
    [Space]
    [SerializeField] private Image[] typeScreens;
    [SerializeField] private GameObject parentTypeScreen;
    [SerializeField] private GameObject parentProvisional;
    [SerializeField] private GameObject parentGeneral;
    [Space]
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;

    private void Start()
    {
        CalculateResolution();
        RecognizeCurrentTypeScreen();

        // -- Initial Values ----
        ancho = Screen.width;
        alto = Screen.height;
        approve = false;
        approveZone.SetActive(false);
    }
    private void Update()
    {
        ChangeVolume();
    }
    // ---- Calculate for Show Resolutions -----------------------------
    private void CalculateResolution()
    {
        Resolution[] resolutions = Screen.resolutions;

        CalculateSizeResolution(resolutions.Length);

        for (int i = 6; i < resolutions.Length; i++)
        {
            // Instanciar resolución
            GameObject newResolution = Instantiate(resolutionPrefab);
            // Modificar su parentesco y tamaño
            newResolution.transform.SetParent(parentResolution.transform);
            newResolution.transform.localScale = new Vector3(1, 1, 1);
            // Guardar el objeto en una lista
            resolutionList.Add(newResolution);

            // Mostrar texto con la resolución especificada
            Text newText = newResolution.GetComponentInChildren<Text>();
            string[] data = resolutions[i].ToString().Split(' ');
            newText.text = data[0] + data[1] + data[2];

            // Guardar el valor que aloja cada resolución
            ResolutionSize size = newResolution.GetComponent<ResolutionSize>();
            size.ancho = int.Parse(data[0]);
            size.alto = int.Parse(data[2]);
            // Guardar su ID
            size.id = resolutionList.Count - 1;

            if(Screen.width == size.ancho)
            {
                currentID = size.id;

                UpdateSelectorResolution(currentID);
            }
        }
    }
    private void CalculateSizeResolution(int cantReslutions)
    {
        RectTransform rect = parentResolution.GetComponent<RectTransform>();

        float dataY = sizeResolutionCube * cantReslutions + (10 * (cantReslutions - 1));

        rect.sizeDelta = new Vector2(0, dataY);
    }
    private void UpdateSelectorResolution(int id)
    {
        // -- Cambiar color del SELECTOR VIEW --
        for(int i = 0; i < resolutionList.Count; i++)
        {
            Image img = resolutionList[i].GetComponent<Image>();
            img.color = Color.white;
        }
        Image imgSelect = resolutionList[id].GetComponent<Image>();
        imgSelect.color = colorSelector;

        // -- Reposicionar --
        RectTransform rect = parentResolution.GetComponent<RectTransform>();
        float newY = (93 * id) + offsetSize;
        rect.transform.localPosition = new Vector3(0, newY, 0);
    }
    // ---- Modificar volumen de Música y SFX --------------------------
    private void ChangeVolume()
    {
        float music = 0;
        float sfx = 0;

        // Establecer valor del volumen a guardar
        music = musicVolume.value;
        sfx = sfxVolume.value;

        // Guardar volumenes como PREFAB
        PlayerPrefs.SetFloat("MusicVolume", music);
        PlayerPrefs.SetFloat("sfxVolume", sfx);
    }
    // ---- Cambia la resolución de la pantalla según el monitor -------
    public void ChangeResolution(int id, int anchoT, int altoT)
    {
        this.id = id;
        lastAlto = altoT;
        lastAncho = anchoT;
        approve = false;

        Screen.SetResolution(anchoT, altoT, completeScreen);

        UpdateSelectorResolution(id);
        StartCoroutine(WaitingToApproveResolution());
    }
    private IEnumerator WaitingToApproveResolution()
    {
        approveZone.SetActive(true);

        for (int i = 10; i > 0; i--)
        {
            if (approve)
            {
                break;
            }
            else
            {
                timerToApprove.text = i.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        if(!approve)
            ResetResolution();
    }
    public void ApproveResolution()
    {
        approve = true;
        ancho = lastAncho;
        alto = lastAlto;
        currentID = id;
        approveZone.SetActive(false);
    }
    public void ResetResolution()
    {
        approve = false;
        Screen.SetResolution(ancho, alto, completeScreen);
        UpdateSelectorResolution(currentID);

        approveZone.SetActive(false);
    }
    // ---- Maneja pantalla completa o modo ventana --------------------
    public void TypeScreen(int index)
    {
        RepositionTypeScreen(index);
        ChangeParent(1);

        // Modificar la pantalla según la selección
        switch (index)
        {
            case 0: completeScreen = FullScreenMode.FullScreenWindow; break; // Pantalla completa en modo ventana
            case 1: completeScreen = FullScreenMode.MaximizedWindow; break;  // Pantalla completa
            case 2: completeScreen = FullScreenMode.Windowed; break; // Modo ventana
        }

        // Aplicar cambios
        Screen.SetResolution(ancho, alto, completeScreen);
    }
    private void RepositionTypeScreen(int index)
    {
        // Colorear el elegido - Apagar el resto
        for (int i = 0; i < typeScreens.Length; i++)
        {
            typeScreens[i].color = Color.white;
        }
        typeScreens[index].color = colorSelector;

        // Modificar la posición del SCROLL RECT
        float newY = 100 * index;
        if (sumOffset)
            newY += offsetSize;
        RectTransform rectangle = parentTypeScreen.GetComponent<RectTransform>();
        rectangle.transform.localPosition = new Vector3(rectangle.transform.localPosition.x, newY, rectangle.transform.localPosition.z);
    }
    private void RecognizeCurrentTypeScreen()
    {
        FullScreenMode fsm = Screen.fullScreenMode;

        switch (fsm)
        {
            case FullScreenMode.FullScreenWindow : RepositionTypeScreen(0); break;
            case FullScreenMode.MaximizedWindow: RepositionTypeScreen(1); break;
            case FullScreenMode.Windowed: RepositionTypeScreen(2); break;
        }
    }
    public void ChangeParent(int id)
    {
        if(id == 0)
        {
            // Quitar del SCROLL RECT PARENT
            parentTypeScreen.transform.SetParent(parentProvisional.transform);
        }
        else
        {
            // Volver al SCROLL RECT PARENT
            parentTypeScreen.transform.SetParent(parentGeneral.transform);
        }
    }
}

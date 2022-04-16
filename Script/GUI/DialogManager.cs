using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    [Header("Variables")]
    [Range(0, 0.25f)] [SerializeField] private float timeToDialog;
    private bool dialogActive;
    private bool inMove;
    private string textDialog;

    [SerializeField] private List<string> dialogLines = new List<string>();
    [SerializeField] private int currentDialogLine;

    [Header("Object")]
    [SerializeField] private GameObject dialogZone;
    [SerializeField] private Text dialog;
    [SerializeField] private Text nextDialog;

    [Header("Calls")]
    private PlayerMovement player;
    private MovementNPC npcGlobal;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        dialogZone.SetActive(false);
        nextDialog.gameObject.SetActive(false);
    }
    private void Update()
    {
        // -- Operador ternario que maneja que el personaje no pueda moverse en las zonas de di�logo ----
        player.canMove = (dialogActive ? false : true);

        // -- Operador ternario que maneja que el NPC no pueda moverse en las zonas de di�logo ----
        if(npcGlobal != null)
        {
            npcGlobal.canMove = (dialogActive ? false : true);
            if(dialogActive)
                npcGlobal.CheckViewingDirection();
        }

        // -- Siguiente di�logo ----
        if (dialogActive && (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) && !inMove)
        {
            // Mostrar siguiente di�logo o finalizar
            currentDialogLine++;

            if(currentDialogLine < dialogLines.Count)
            {
                nextDialog.gameObject.SetActive(false);
                StartCoroutine(ShowText());
            }
        }
        // -- Ya se mostraron todos los di�logos ----
        if (currentDialogLine >= dialogLines.Count && currentDialogLine > 0)
        {
            ResetToDefault();
        }

        // -- Apurar el muestreo de los di�logos ----
        if (inMove && (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)))
        {
            inMove = false;
            StopCoroutine(ShowText());
            dialog.text = dialogLines[currentDialogLine];
            nextDialog.gameObject.SetActive(true);
        }
    }
    private void ResetToDefault()
    {
        dialogActive = false;
        dialogZone.SetActive(false);
        currentDialogLine = 0;

        // Vaciar la lista de di�logos guardados
        dialogLines.Clear();
    }
    public void ShowDialog(string[] text, MovementNPC npc = null)
    {
        if (!dialogActive)
        {
            for(int i = 0; i < text.Length; i++)
            {
                dialogLines.Add(text[i]);
            }
            
            npcGlobal = npc;
            dialogActive = true;
            dialogZone.SetActive(true);

            StartCoroutine(ShowText());
        }
    }
    private IEnumerator ShowText()
    {
        // Vaciar texto para comenzar de nuevo
        dialog.text = "";
        // Espera inicial antes de cargar el texto
        yield return new WaitForSeconds((timeToDialog * 2.5f));
        // Activar movimiento de texto
        inMove = true;

        for (int i = 0; i < dialogLines[currentDialogLine].Length; i++)
        {
            // Rompe el bucle si se desactiva su movimiento
            if (!inMove)
                break;

            dialog.text += dialogLines[currentDialogLine][i];
            yield return new WaitForSeconds(timeToDialog);
        }
        inMove = false;
        nextDialog.gameObject.SetActive(true);
    }
}

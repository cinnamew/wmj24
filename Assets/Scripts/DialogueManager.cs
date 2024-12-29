using UnityEngine;
using Fungus;

public class DialogueManager : Singleton<DialogueManager>
{

    [SerializeField] private Fungus.DialogInput _dialogInput;
    [SerializeField] private Fungus.Writer _writer;

    public void ChangeDialogInputClickMode(ClickMode clickMode) => _dialogInput.clickMode = clickMode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void ChangeWritingSpeed(float f)
    {
        _writer.ChangeWritingSpeed(f);
    }


    public void RunBlock(string blockName)
    {

    }
}

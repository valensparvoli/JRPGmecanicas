using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int letterPerSecond;
    [SerializeField] Color highlightcolor;

    [SerializeField] Text dialogText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTexts;

    [SerializeField] Text mtText;
    [SerializeField] Text typeText;



    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog) //Funcion encargada de escribir los textos letra por letra
    {
        dialogText.text = "";
        foreach(var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        yield return new WaitForSeconds(1f);

    }
    public void EnableDialogText(bool enable) //funcion que activa y desactiva el dialogText
    {
        dialogText.enabled = enable;
    }

    public void EnableActionSelector(bool enable)//funcion que activa y desactiva el actvionSelector
    {
        actionSelector.SetActive(enable);
    }

    public void EnableMoveSelector(bool enable)//funcion que activa y desactiva el moveSelector
    {
        moveSelector.SetActive(enable);
        moveDetails.SetActive(enable);
    }

    public void UpdateActionSelection(int selectedAction) //funcion que colorea la accion que estemos seleccionando segun su valor
    {
        for(int i = 0; i < actionTexts.Count; ++i)
        {
            if (i == selectedAction)
                actionTexts[i].color = highlightcolor;
            else
                actionTexts[i].color = Color.black;
        }
    }

    public void UpdateMoveSelection(int selectedMove, Moves move) //funcion que colorea el movimiento que estemos seleccionando segun su valor
    {
        for (int i = 0; i < moveTexts.Count; ++i)
        {
            if (i == selectedMove)
                moveTexts[i].color = highlightcolor;
            else
                moveTexts[i].color = Color.black;
        }

        mtText.text="MT " + move.MOVETIMES + " / " + move.Base.MovesTimes;
        typeText.text = move.Base.Type.ToString();
    }

    public void SetMoveNames(List<Moves> moves) //Funcion que escribe los nombres de cada movimiento segun su objeto
    {
        for(int i=0; i < moveTexts.Count; ++i)
        {
            if (i < moves.Count)
                moveTexts[i].text = moves[i].Base.MoveName;
            else
                moveTexts[i].text = "-";
        }
    }

}

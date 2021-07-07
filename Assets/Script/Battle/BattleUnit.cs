using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    
    [SerializeField] bool isPlayerUnit;

    public Enemy enemy { get; set; }

    Image image;
    Vector3 originalPos;
    Color originalColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void SetUp(Enemy _enemy) //Establece que tipo de imagen se cargara cuando comience la batalla
    {
        enemy = _enemy;
        if (isPlayerUnit)
            image.sprite = enemy.Base.BackSprite;
        else
            image.sprite = enemy.Base.FrontSprite;

        image.color = originalColor;
        PlayEnterAnimation();
    }

    public void PlayEnterAnimation()
    {
        if (isPlayerUnit)
        {
            image.transform.localPosition = new Vector3(-500f, originalPos.y);//posicion fuera de la camara
        }
        else
        {
            image.transform.localPosition = new Vector3(500f, originalPos.y);//entra en la pelea
        }

        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();//Crea secuencias
        if (isPlayerUnit)
        {
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f)); //mueve hacia delante
        }
        else
        {
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f)); //mueve hacia atras
        }

        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));//restablece la posicion
    }

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence(); //Crea secuencias
        sequence.Append(image.DOColor(Color.gray, 0.1f)); //Cambia el color 
        sequence.Append(image.DOColor(originalColor, 0.1f)); //restablece el color 
    }

    public void PlaySurrendedAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f,0.5f));
    }
}

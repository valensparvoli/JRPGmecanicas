using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy } //Busy state es cuando el player y el enemigo estan atacando

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHUD playerHud;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    BattleState state;
    int currentAction;
    int currentMove;

    private void Start()
    {
        StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle() //Corrutina que da comienzo a la pelea
    {
        playerUnit.SetUp();
        enemyUnit.SetUp();
        playerHud.SetData(playerUnit.enemy);
        enemyHud.SetData(enemyUnit.enemy);

        dialogBox.SetMoveNames(playerUnit.enemy.moves);

        yield return dialogBox.TypeDialog("A wild " + enemyUnit.enemy.Base.Name + " has appeared");


        PlayerAction();

    }

    void PlayerAction() //Acciones realizadas en UI en Player Action
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Chose an action"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove() //Acciones de UI realizadas en PlayerMove
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove() //Corrutina que maneja el turno del player
    {
        state = BattleState.Busy; // Cambiamos el estado para poder ejecutar la accion

        var move = playerUnit.enemy.moves[currentMove]; //Guarda el movimiento a utilizar
        yield return dialogBox.TypeDialog(playerUnit.enemy.Base.name + " used " + move.Base.MoveName);
        


        var damageDetails= enemyUnit.enemy.TakeDamage(move, playerUnit.enemy); //Hace daño y chequea si el enemigo se rindio
        yield return enemyHud.UpdateHP(); //Modifica la barra de vida del enemigo
        yield return ShowDamageDetails(damageDetails); //Influye en el dialogo de la accion relazida (Critico o efectivo)

        if (damageDetails.Surrended)
        {
            yield return dialogBox.TypeDialog(enemyUnit.enemy.Base.Name + " get surrended");
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove() //Corrutina que maneja el turno enemigo
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.enemy.GetRandomMove();
        yield return dialogBox.TypeDialog(enemyUnit.enemy.Base.name + " used " + move.Base.MoveName);

        var damageDetails = playerUnit.enemy.TakeDamage(move, playerUnit.enemy);
        yield return playerHud.UpdateHP(); //Quita vida al playerUnit
        yield return ShowDamageDetails(damageDetails); //Influye en el dialogo de la accion relazida (Critico o efectivo)

        if (damageDetails.Surrended)
        {
            yield return dialogBox.TypeDialog(playerUnit.enemy.Base.Name + " get surrended");
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
        {
            yield return dialogBox.TypeDialog("A critical hit!");
        }
        if (damageDetails.TypeEffectiveness > 1)
        {
            yield return dialogBox.TypeDialog("It is super effective!");
        }
        else if (damageDetails.TypeEffectiveness < 1f)
        {
            yield return dialogBox.TypeDialog("It is not very effective!");
        }
    }

    private void Update()//Checkea los diferentes estados del player
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection() //Funcion encargada de elegir la accion que le interesa realizar al jugador "Fight" o "Run"
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentAction<1)
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
                --currentAction;
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentAction == 0)
            {
                //Fight
                PlayerMove();
            }
            if (currentAction == 1)
            {
                //Run
            }
        }
    }

    void HandleMoveSelection() //Funcion encargada de elegir el movimiento que le interesa realizar al jugador dentro de fight
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.enemy.moves.Count-1)
                ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
                --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.enemy.moves.Count-2)
                currentMove+=2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove < 1)
                currentMove -= 2;
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.enemy.moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);

            StartCoroutine(PerformPlayerMove());
        }
    }
}

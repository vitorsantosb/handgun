using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum STATE_GAME
    {
        INITIALIZING,
        READY_TO_GO,
        ROLLING_DICES,
        CHANGE_USER_INFO,
        STARTING,
        CHECKING_DICES,
        SPAWNPLAYER,
        START_TURN,
        FIRST_ROUND,
        NEXT_ROUND,
        END_ROUND,
        UPDATE_GAME_VALUES,
        CHECK_PLAYER_DOWN,
        REBOOTING, 
    }
}

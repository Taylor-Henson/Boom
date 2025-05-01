using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyTrigger : MonoBehaviour
{
    public StationaryEnemy stationaryEnemyScript;
    public EnemyCombat enemyCombatScript;
    public Rig rig;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerHolder" && !enemyCombatScript.dead)
        {
            rig.weight = 1;
            stationaryEnemyScript.inSight = true;
            stationaryEnemyScript.player = other.gameObject;
            enemyCombatScript.playerCombatScript = other.gameObject.GetComponent<PlayerCombat>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayerHolder" && !enemyCombatScript.dead)
        {
            rig.weight = 0;
            stationaryEnemyScript.inSight = false;
        }
    }
}

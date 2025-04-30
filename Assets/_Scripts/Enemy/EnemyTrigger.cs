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
            print(rig.weight);
            stationaryEnemyScript.inSight = true;
            stationaryEnemyScript.player = other.gameObject;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayerHolder" && !enemyCombatScript.dead)
        {
            rig.weight = 0;
            print(rig.weight);
            stationaryEnemyScript.inSight = false;
        }
    }
}

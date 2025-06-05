using UnityEngine;
using System.Collections;

public class TelelportingParticals : MonoBehaviour
{
    public IEnumerator MoveTowrad(Vector2 pos, float speed){
        // wait until arrival
        yield return new WaitUntil(()=> {
            transform.position = Vector2.Lerp(transform.position, pos, speed * Time.deltaTime);
            return ((Vector2) transform.position - pos).magnitude <= 0.1f;
        });

        // stop the particals and delete
        GetComponent<ParticleSystem>().Stop();
    }
}

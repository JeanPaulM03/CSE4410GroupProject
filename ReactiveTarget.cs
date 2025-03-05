using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{   
    [SerializeField] private ParticleSystem _particles;
    public Coroutine deathAnim { private set; get;}

    // If this target gets hit, react to it
    public void ReactToHit() {
        // Get reference to wandering AI script 
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null) {
            behavior.SetAlive(false);
        }


        // Death status
        if (deathAnim == null) deathAnim = StartCoroutine(Die());
    }

    // Death animation, as a coroutine 
    public IEnumerator Die() {
        // Have the target fall over to the side
        this.transform.Rotate(-75, 0, 0);

        // Turn on particles 
        _particles.enableEmission = true;

        // Then wait
        yield return new WaitForSeconds(1.5f);

        // Then despawn by destorying itself 
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
       // _particles.enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

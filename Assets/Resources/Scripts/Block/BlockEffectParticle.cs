using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Particle Effect on Blocks.
// WIP

public class BlockEffectParticle : MonoBehaviour
{
    GameObject specialParticle;

    void Awake()
    {
        initiateSpecial(GetComponent<Block>().blockinfo.blockname);
    }

    private void initiateSpecial(string blockname)
    {
        switch (blockname)
        {
            case "Money Bundle":
                specialParticle = Instantiate(Resources.Load("Prefab/BlockParticle")) as GameObject;
                specialParticle.transform.position = transform.position;
                specialParticle.GetComponent<ParticleSystemRenderer>().material = Resources.Load("shader/ParticleEffects/DollarParticle") as Material;
            break;

            case "XP Block":
                specialParticle = Instantiate(Resources.Load("Prefab/BlockParticle")) as GameObject;
                specialParticle.transform.position = transform.position;
                specialParticle.GetComponent<ParticleSystemRenderer>().material = Resources.Load("shader/ParticleEffects/SparkParticle") as Material;
            break;

            case "Crate-F":
            case "Crate-E":
            case "Crate-D":
            case "Crate-C":
            case "Crate-B":
            case "Crate-A":
                specialParticle = Instantiate(Resources.Load("Prefab/BlockParticle")) as GameObject;
                specialParticle.transform.position = transform.position;
                specialParticle.GetComponent<ParticleSystemRenderer>().material = Resources.Load("shader/ParticleEffects/BoxParticle") as Material;
            break;

            default:
                Debug.LogError("Special for" + blockname + "not declared or mispelled!");
            break;
        }
    }
}
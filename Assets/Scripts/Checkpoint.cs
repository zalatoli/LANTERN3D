using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public HealthManager theHealthManager;

    public Renderer theRenderer;

    public Material checkpointOnMaterial;
    public Material checkpointOffMaterial;

	// Use this for initialization
	void Start () {
        theHealthManager = FindObjectOfType<HealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckpointOn()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint cp in checkpoints)
        {
            cp.CheckpointOff();
        }
        theRenderer.material = checkpointOnMaterial;
    }

    public void CheckpointOff()
    {
        theRenderer.material = checkpointOffMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            theHealthManager.SetSpawnPoint(transform.position);
            CheckpointOn();
        }
    }
}

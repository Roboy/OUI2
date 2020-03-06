using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstructFXManager : MonoBehaviour {
    [SerializeField] private Material constructEffectMaterial;
    [SerializeField] private Material constructEffectMaterialArt;

    private GameObject[] constructEffectObjects;

    private GameObject[] constructEffectObjectsArt;

    // Start is called before the first frame update
    void Start() {
        constructEffectObjects = GameObject.FindGameObjectsWithTag("ConstructFX");
        constructEffectObjectsArt = GameObject.FindGameObjectsWithTag("ConstructFXArt");
    }

    public void toggleEffects() {
        foreach (var constructEffectObject in constructEffectObjects) {
            MeshRenderer meshRenderer = constructEffectObject.GetComponent<MeshRenderer>();

            Material prevMaterial = meshRenderer.materials[0];


            if (meshRenderer.materials.Length == 1) {
                // In this case, normal mode is currently activated. Only standard material.
                meshRenderer.materials = new Material[] {
                    prevMaterial,
                    constructEffectMaterial
                };
            }
            else {
                // In this case, construct mode is currently activated.
                meshRenderer.materials = new Material[] {
                    prevMaterial
                };
            }
        }

        foreach (var constructEffectObject in constructEffectObjectsArt) {
            constructEffectObject.SetActive(!constructEffectObject.activeSelf);
        }
    }
}
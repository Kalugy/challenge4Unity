using System;
using UnityEngine;

public class Pet
{
    // Properties
    public string Name { get; set; }
    public int Happiness { get; set; }

    // Constructor
    public Pet(string name, int happiness)
    {
        Name = name;
        Happiness = happiness;
    }

    // Method to increase happiness
    public void Play(int happiness)
    {
        Happiness += happiness;
    }
}

public class Game : MonoBehaviour
{
    private void Start()
    {
        Pet myPet = new Pet("Buddy", 0);
        myPet.Name = "Doggy";
        myPet.Play(5);
        Debug.Log("My Pet:" + " " + myPet.Name + " " + myPet.Happiness);
        myPet.Name = "Test";
        myPet.Play(5);
        Debug.Log("My Pet:" + " " + myPet.Name + " " + myPet.Happiness);

    }
}
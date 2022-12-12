using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInfo : MonoBehaviour
{
    public enum ShipVenture
    {
        Freight, Transportation, Habitation, Production, Mining,
        Scrapping, Military, Research, Sampling, ShipHaul
    }

    public int Population;
    public int Density;
    public ShipVenture Venture;

    //public 
}

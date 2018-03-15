using GameState;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TopographyManager : Topography {

    private const int NULL_VALUE = -1;

    public TopographyManager(int size) {
        highestLevel = 1;
        topography = new int[size];
        for (int i = 0; i < size; ++i) {
            topography[i] = NULL_VALUE;
        }
    }

    private TopographyManager(Topography topography) {

    }

    private void CalculateHighestLeveL(int value) {
        highestLevel = Mathf.Max(highestLevel, CalculateLeveL(value));
    }

    private int CalculateIndex(int value) {
        return value % topography.Length;
    }

    private int CalculateLeveL(int value) {
        return (int)Mathf.Floor(value / topography.Length);
    }

    public bool Contains(int value) {
        if (topography[CalculateIndex(value)] >= 0)
            return true;
        return false;
    }

    public int Find(int value) {
        return topography[CalculateIndex(value)];
    }

    public int HighestLevel {
        get { return highestLevel; }
        private set { highestLevel = value; }
    }

    public Topography SerializedData {
        get {
            Topography topo = new Topography();

            return topo;
        }
    }

    public void Set(int value) {
        if (value >= 0)
            topography[CalculateIndex(value)] = value;
        else
            topography[CalculateIndex(value)] = NULL_VALUE;
        
        CalculateHighestLeveL(value);
    }
}

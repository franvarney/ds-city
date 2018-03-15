using GameState;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MapNode : Cube {

    public MapNode(int i, int l, Vector3? p = null) {
        index = i;
        xz = i;
        y = l;
        range = 1;
        position = p == null ? Vector3.zero : (Vector3)p;
        neighborIds = new List<int>();
    }

    public MapNode(Cube cube) {
        Type type = cube.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo field in fields) {
            MapNodeType.GetField(field.Name).SetValue(this, field.GetValue(cube.GetType()));
        }
    }

    public void AddNeighborId(int id) {
        neighborIds.Add(id);
    }

    public List<int> NeighborIdsInSameAxes {
        get {
            List<int> ids = new List<int>();
            bool keepSearching = true;
            int i = 0;

            while (keepSearching) {
                keepSearching = false;


            }

            return ids;
        }
    }

    public int BackId { get { return CalculateId(Vector3Int.FloorToInt(Vector3.back)); } } // [0, 0, -1]

    private int CalculateId(Vector3Int direction) {
        int midpoint = index - (+xz);
        int size = midpoint * 2;
        size += midpoint % 2 == 0 ? 1 : 2;
        return (size * (y + direction.y)) + ((index * direction.z) + direction.x);
    }

    public int EastId { get { return CalculateId(Vector3Int.right); } } // [1, 0, 0]
    public int FrontId { get { return CalculateId(Vector3Int.FloorToInt(Vector3.forward)); } } // [0, 0, 1]

    public void IncrementRange() {
        range += 1;
    }

    public int Id { get { return CalculateId(Vector3Int.zero); } }
    public int Level { get { return y; } }
    private Type MapNodeType { get { return GetType(); } }
    public List<int> NeighborIds { get { return neighborIds; } }
    public int NorthId { get { return CalculateId(Vector3Int.up); } } // [0, 1, 0]

    public List<int> PotentialNeighborIdsInRange { // TODO
        get {
            List<int> ids = new List<int>();

            for (int i = 0; i < range; ++i) {
                ids.Add(CalculateId(Vector3Int.FloorToInt(Vector3.back)));
                ids.Add(CalculateId(Vector3Int.right));
                ids.Add(CalculateId(Vector3Int.FloorToInt(Vector3.forward)));
                ids.Add(CalculateId(Vector3Int.down));
                ids.Add(CalculateId(Vector3Int.left));
            }

            return ids;
        }
    }

    public void RemoveNeighborId(int id) {
        neighborIds.RemoveAt(id);
    }

    public Cube SerializedData {
        get {
            Cube cube = new Cube();
            Type type = cube.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fields) {
                type.GetField(field.Name).SetValue(cube, field.GetValue(MapNodeType));
            }

            return cube;
        }
    }

    public int SouthId { get { return CalculateId(Vector3Int.down); } } // [0, -1, 0]
    public int WestId { get { return CalculateId(Vector3Int.left); } } // [-1, 0, 0]

}


using GameState;
using System;
using Object = System.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapManager : CubeMap {

    private Dictionary<int, MapNode> nodes = new Dictionary<int, MapNode>();
    private TopographyManager topography;

    public MapManager(int xz) {
        height = xz;
        width = xz;
        size = xz * xz;
        topography = new TopographyManager(size);
    }

    public MapManager(CubeMap map) {

    }

    public bool Contains(int id) {
        return nodes.ContainsKey(id);
    }

    public int Count { get { return nodes.Count; } }

    public void Add(MapNode node) {
        nodes.Add(node.Id, node);
        Topography.Find(node.Id);
        // reverse search for neighbors that have range that cover this node
    }

    public void Add(int i, int l, Vector3 p) {
        MapNode node = new MapNode(i, l, p);
        Topography.Find(node.Id);
        nodes.Add(node.Id, node);
        // reverse search for neighbors that have range that cover this node
    }

    public MapNode Find(MapNode node) {
        return nodes[node.Id];
    }

    public MapNode Find(int id) {
        return nodes[id];
    }

    public bool Remove(MapNode node) {
        if (nodes.ContainsKey(node.Id)) {
            RemoveNodeNeighbors(node);
            Topography.Find(node.SouthId);
            nodes.Remove(node.Id);
            return true;
        }
        return false;
    }

    public bool Remove(int id) {
        if (nodes.ContainsKey(id)) {
            MapNode node = Find(id);
            RemoveNodeNeighbors(node);
            Topography.Find(node.SouthId);
            nodes.Remove(node.Id);
            return true;
        }
        return false;
    }

    private void RemoveNodeNeighbors(MapNode node) {
        List<int> neighborIds = node.NeighborIds;
        foreach (int id in neighborIds) {
            if (neighborIds.Contains(node.Id)) {
                nodes[id].RemoveNeighborId(node.Id);
            }
        }
    }

    private List<int> SearchNodeAxes(MapNode node) {
        List<int> ids = new List<int>();
        bool keepSearching = true;
        int i = 0;

        while (keepSearching) {
            keepSearching = false;

            if (((node.FrontId + (size * i)) / size) == node.Level || ((node.BackId + (size * i)) / size) == node.Level) {

            }

            ++i;
        }

        return ids;
        //top = 0 to null
        //side = mod 0 to size
        //front = is id/size ceil  
    }

    public CubeMap SerializedData {
        get {
            CubeMap map = new CubeMap();

            return map;
        }
    }

    public TopographyManager Topography { get { return topography; } }

}
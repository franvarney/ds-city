using GameState;
using Object = System.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private const float VERTICAL_SPACING = 5.0f;
    private const float HORIZONTAL_SPACING = 7.5f;
    private Vector3[] DIRECTIONS = new Vector3[5] { Vector3.up, Vector3.back, Vector3.forward, Vector3.left, Vector3.right };
   
    private bool placeHolderIsActive = false;
    private CubeActive active;
    [SerializeField] private CubeActive activePrefab;
    [SerializeField] private GameObject Land;
    [SerializeField] private GameObject startingNode;
    [SerializeField] private int mapSize = 5;
    private List<CubeActive> placeHolderCubes = new List<CubeActive>();
    private MapManager map;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material placeHolderMaterial;
    private RaycastHit parentHit;

    private void Awake() {
        if (Game.inProgress) {
            // rebuild
        } else {
            map = new MapManager(mapSize);
            int middle = (int)Mathf.Floor((mapSize * mapSize) / 2);
            startingNode.GetComponent<CubeActive>().id = middle;
            MapNode node = new MapNode(middle, 1, startingNode.transform.position);
            map.AddNode(node);
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 position = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                switch (hit.transform.tag) {
                    case "CubeTop": AddPlaceHolder(hit); break;
                    case "CubePlaceHolder": AddActive(hit); break;
                    default:
                        break;
                }
            }
        }

        /*private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)) {
                    if (hit.transform.tag == "CubeTop") {

                    }

                    if (hit.transform.tag == "CubePlaceHolder") {

                    }
                }
            }

            // if clicked elsewhere then ReturnToPool();
            //Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
            //Stuff spawn = prefab.GetPooledInstance<Stuff>();
        }*/
    }

    private void AddPlaceHolder(RaycastHit hit) {
        if (!placeHolderIsActive) {
            Transform parentTransform = hit.transform;
            CubeActive parentCube = parentTransform.GetComponent<CubeActive>();
            Debug.Log("id3");
            Debug.Log(parentCube.id);
            Debug.Log(map.Topography.GetValue(parentCube.id));
            Debug.Log(map.Count);
            Debug.Log(map.Topography.GetValue(13));

            if (!map.Contains(parentCube.id)) {
                // TODO Throw error
            }

            MapNode hitNode = map.FindNodeById(parentCube.id);
            Object[] availableSpaces = map.GetAvailableNodeIdsInRange(hitNode);
            Vector3 position = parentTransform.position;

            for (int i = 0; i < availableSpaces.Length; ++i) {
                active = activePrefab.GetPooledInstance<CubeActive>();
                if (availableSpaces[i] != null && (int)availableSpaces[i] >= 0) {
                    position = map.nodes[(int)availableSpaces[i]].position;
                }   
                Vector3 direction = new Vector3(hitNode.id + 1 * DIRECTIONS[i].x, hitNode.id + Mathf.Ceil(hitNode.id / map.size) * DIRECTIONS[i].y, hitNode.id + map.width * DIRECTIONS[i].z);
                active.Spawn(availableSpaces[i], position, DIRECTIONS[i]);
                active.SetMaterial("placeholder");
                active.SetId(hitNode.id, direction);
                Debug.Log("new");
                Debug.Log(active.id);
                placeHolderCubes.Add(active);
            }

            placeHolderIsActive = true;
        }
    }

    private void AddActive(RaycastHit hit) {
        if (placeHolderIsActive) {
            for (int i = 0; i < placeHolderCubes.Count; ++i) {
                CubeActive cube = placeHolderCubes[i];
                if (hit.transform.position == cube.transform.position) {
                    cube.SetMaterial("active");
                    cube.transform.tag = "CubeTop";
                    MapNode node = new MapNode(cube.id % map.size, (int)Mathf.Ceil(cube.id / map.size), cube.transform.position);
                    map.AddNode(node);
                } else {
                    cube.ReturnToPool();
                }
            }

            placeHolderCubes.Clear();
            placeHolderIsActive = false;

            /*parentHit.transform.tag = "Cube";
            CubeActive active = activePrefab;
            active = active.GetPooledInstance<CubeActive>();
            active.Spawn(placeHolder.transform);
            placeHolder.ReturnToPool();
            placeHolderIsActive = false;*/
        }
    }

    // xray
    // visualizerow
    // visualizecolumn
    // visualizelevel
    // updateBuilding
    // selectBuilding (show area of effect)
}

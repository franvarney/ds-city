using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameState {
    [Serializable]
    public class CubeMap {
        public int height;
        public int size;
        public int width;
        public List<Cube> cubes;
        public int[] topographyIds;
    }
}

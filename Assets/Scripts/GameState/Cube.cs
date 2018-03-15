using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameState {
    [Serializable]
    public class Cube {
        protected int index;
        protected int range;
        protected int xz;
        protected int y;
        protected List<int> neighborIds;
        protected Vector3 position;
    }
}

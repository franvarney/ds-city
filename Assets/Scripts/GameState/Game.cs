using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameState {
    public sealed class Game {

        private static Game instance;

        public static bool inProgress = false;

        public static Game Instance {
            get {
                return instance;
            }
            set {
                instance = value;
            }
        }
    }
}

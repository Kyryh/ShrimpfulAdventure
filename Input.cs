using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure {
    internal static class Input {
        static KeyboardState kstate = default;
        static KeyboardState oldKstate = default;
        public static void Update() {
            oldKstate = kstate;
            kstate = Keyboard.GetState();
        }

        static bool KeyPressed(Keys key, bool thisFrame) {
            return kstate[key] == KeyState.Down && (!thisFrame || oldKstate[key] == KeyState.Up);
        }
        public static bool JumpPressed() {
            return KeyPressed(Keys.Up, true) || KeyPressed(Keys.W, true);
        }

        public static bool LeftPressed() {
            return KeyPressed(Keys.Left, false) || KeyPressed(Keys.A, false);
        }

        public static bool RightPressed() {
            return KeyPressed(Keys.Right, false) || KeyPressed(Keys.D, false);
        }
    }
}

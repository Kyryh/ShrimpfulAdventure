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

        static bool KeyPressed(Keys key) {
            return oldKstate[key] == KeyState.Up && kstate[key] == KeyState.Down;
        }
        public static bool JumpPressed() {
            return KeyPressed(Keys.Up) || KeyPressed(Keys.W);
        }
    }
}

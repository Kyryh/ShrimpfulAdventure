using KEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class Interactable : Component {
        public Func<int, int> OnInteract { private get; init; }
        int state = 0;
        public void Interact() {
            state = OnInteract.Invoke(state);
        }
    }
}

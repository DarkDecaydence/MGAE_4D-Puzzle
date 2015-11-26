using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts_v02.FourthDimension {
    interface IFourthDimension {
        void SetW(int newW);
        void PushW(int wDiff);
        bool CanGoWUp();
        bool CanGoWDown();
    }
}

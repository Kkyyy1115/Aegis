using Aegis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEngine : MonoBehaviour
{
    // Start is called before the first frame update
    public static float GetTypeFactor(EffectTypes projectileType, EffectTypes shieldType) {

        switch (projectileType) {
            case EffectTypes.Kinetic:
                switch (shieldType) {
                    case EffectTypes.Kinetic:
                        return 1.0f;
                    case EffectTypes.Energy:
                        return 0.5f;
                    case EffectTypes.Arcane:
                        return 2.0f;
                }
                break;
            case EffectTypes.Energy:

                switch (shieldType) {
                    case EffectTypes.Kinetic:
                        return 2.0f;
                    case EffectTypes.Energy:
                        return 1f;
                    case EffectTypes.Arcane:
                        return 0.5f;
                }
                break;
            case EffectTypes.Arcane:

                switch (shieldType)
                {
                    case EffectTypes.Kinetic:
                        return 0.5f;
                    case EffectTypes.Energy:
                        return 2f;
                    case EffectTypes.Arcane:
                        return 1f;
                }
                break;

            default:
                return 0;

        }
        return 0;
    }
}

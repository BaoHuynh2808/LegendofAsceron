using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Expression  {
    public static int GetExpExpression (int level) {
        if (level == 1)
        {
            return 0;
        }
        int requiredExp = Mathf.FloorToInt((4 / 3) * Mathf.Pow(level, 3.0f) -
                           (15 * Mathf.Pow(level, 2.0f)) + (80 * level) - 102);
        return requiredExp;
	}

	private static float PATK (float strength, float weaponPATK) {
		return weaponPATK + strength;
	}

	private static float Modifier (float CRTD, float resistance) {
		return CRTD * resistance;
	}


	public static float SkillATK (float strength, float weaponATK,float skillDamageScale, float DEFtarget, float CRTD, float resistance) {
		return (PATK(strength,weaponATK) * skillDamageScale) * 100 / (100 + DEFtarget) * Modifier(CRTD,resistance);
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildPreProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        //EditorSettings.spritePackerMode = SpritePackerMode.BuildTimeOnly;
        //UnityEditor.Sprites.Packer.RebuildAtlasCacheIfNeeded(EditorUserBuildSettings.activeBuildTarget, false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class vfx_Manager : Singleton<vfx_Manager>
{
    public enum VFXType
    {
        JUMP,
        VFX_2
    }

    public List<VFXManagerSetup> vfxSetup;

    public void PlayVFXByType(VFXType vfxType, Vector3 position) {
        foreach(var i in vfxSetup)
        {
            if(i.vfxType == vfxType)
            {
                var item = Instantiate(i.prefab);
                item.transform.position = position;
                Destroy(item.gameObject, 3f);
                break;
            }
        }
    }
}

[System.Serializable]
public class VFXManagerSetup
{
    public vfx_Manager.VFXType vfxType;
    public GameObject prefab;
}
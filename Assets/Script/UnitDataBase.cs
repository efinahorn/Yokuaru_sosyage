using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitDataBase", menuName = "CreateUnitDataBase")]
public class UnitDataBase : ScriptableObject
{
    public List<UnitStatus> unitStatuses = new List<UnitStatus>();

    public UnitStatus findUnitId(int id)
    {
        for (int i = 0; i < unitStatuses.Count; i++)
        {
            UnitStatus st = unitStatuses[i];
            if (st == null) continue;  //  null�̏ꍇ������
            if (st.getID() == id)
            {
                return st;
            }
        }
        Debug.Log("�Ȃ�������BID=" + id);
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成されるタイルのクラス
/// </summary>
public class TileData : MonoBehaviour
{
    //壁ならtrue
    bool _isWall;
    //上への階段ならtrue
    bool _isStepUp;
    //下への階段ならtrue
    bool _isStepDown;

    public TileData()
    {

    }
}

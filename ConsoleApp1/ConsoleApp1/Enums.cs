using System;

namespace Enums
{
    public enum ViewField
    {
        Title,
        SelectJob,
        Lobby,
        Town,
        Field,
        Battle,
        Inn
    }

    public enum Tile
    {
        None, // 없음 x
        Floor, // 바닥 o
        Wall, // 벽 x
        Potal, // 포탈 o E
        Item, // 아이템 o E
        Monster, // 몬스터 o E
        Casino,
        End,
    }

    public enum BattleAction
    {
        None,
        Attack, // 공격
        RunAway, // 도망
    }

    public enum PlayerAttackType
    {
        None,
        Miss,
        Attack,
        DoubleAttack,
        CriticalAttack,
    }
}
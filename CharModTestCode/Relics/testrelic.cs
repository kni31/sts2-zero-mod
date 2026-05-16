using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CharModTest.CharModTestCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rooms;

namespace CharModTest.CharModTestCode.Relics;

[BaseLib.Utils.Pool(typeof(SharedRelicPool))]
public class MyRelic : CharModTestRelic
{
    public override RelicRarity Rarity => RelicRarity.Common;
    public int RarityRank => 0;
    public int MaxStack => 1;
    public bool IsSeen => true;
    public bool IsSeenInShop => true;
    public bool IsObtained => true;
    public bool IsActive => true;

    public string Description => "This is a relic.";
    public string Name => "My Relic";

    public string CustomTexturePath => "my_relic.png".RelicImagePath();

    public override async Task AfterObtained()
    {
        if (Owner?.Creature != null)
        {
            await PowerCmd.Apply<StrengthPower>(Owner.Creature, 1m, Owner.Creature, null, false);
        }

        await base.AfterObtained();
    }

    public override async Task AfterRemoved()
    {
        if (Owner?.Creature != null)
        {
            await PowerCmd.Apply<StrengthPower>(Owner.Creature, -1m, Owner.Creature, null, false);
        }

        await base.AfterRemoved();
    }
    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is CombatRoom)
        {
            Flash();
            // 직접 1m(데시멀 1)을 전달합니다.
            await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, 1m, base.Owner.Creature, null);
        }
    }
}
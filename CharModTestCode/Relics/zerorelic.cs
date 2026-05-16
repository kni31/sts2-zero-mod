using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CharModTest.CharModTestCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using static MegaCrit.Sts2.Core.Models.RelicModel;

namespace CharModTest.CharModTestCode.Relics;

[BaseLib.Utils.Pool(typeof(SharedRelicPool))]
public class ZeroRelic : CharModTestRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    public int RarityRank => 0;
    public int MaxStack => 1;
    public bool IsSeen => true;
    public bool IsSeenInShop => true;
    public bool IsObtained => true;
    public bool IsActive => true;

    public string Description => "카드를 사용할 때마다 별을 3개 얻습니다.";
    public string Name => "제로의 모래시계";

    public string CustomTexturePath => "zerorelic.png".RelicImagePath();

    // Ensure this relic uses the specific zerorelic images instead of depending on Id-based defaults
    public override string PackedIconPath => "zerorelic.png".RelicImagePath();
    protected override string PackedIconOutlinePath => "zerorelic_outline.png".RelicImagePath();
    protected override string BigIconPath => "zerorelic.png".BigRelicImagePath();

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new StarsVar(3) };

    public override async Task AfterObtained()
    {
        await base.AfterObtained();
    }

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        await base.AfterRoomEntered(room);
    }

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (Owner?.Creature != null && cardPlay.Card.Owner == Owner.Creature.Player)
        {
            await PowerCmd.Apply<Zeropower>(Owner.Creature, 1m, Owner.Creature, null);
        }

        await base.BeforeCardPlayed(cardPlay);
    }
}

public sealed class Zeropower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override string CustomPackedIconPath => "zeropower.png".PowerImagePath();
    public override string CustomBigIconPath => "zeropower.png".BigPowerImagePath();

    // Power count is displayed by the power system itself, so no extra dynamic vars are required here.
}


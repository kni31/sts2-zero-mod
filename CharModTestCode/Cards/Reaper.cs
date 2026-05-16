using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Cards;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models.CardPools;
using Godot;
using CharModTest.CharModTestCode.Character;

namespace CharModTest;

[Pool(typeof(CharModTestCardPool))]
public class Reaper() : CharModTest.CharModTestCode.Cards.CharModTestCard(2, CardType.Attack, 
    CardRarity.Rare, TargetType.AllEnemies) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(4m, DamageProps.card) };

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(CombatState, "CombatState");
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    public override async Task AfterDamageGiven(
        PlayerChoiceContext choiceContext,
        Creature? dealer,
        DamageResult result,
        ValueProp props,
        Creature target,
        CardModel? cardSource
    )
    {
        if(cardSource == this && result.UnblockedDamage > 0)
        {
            await CreatureCmd.Heal(base.Owner.Creature, result.UnblockedDamage);
        }
    }

    protected override void OnUpgrade() {
        base.DynamicVars.Damage.UpgradeValueBy(1m);
    }
}
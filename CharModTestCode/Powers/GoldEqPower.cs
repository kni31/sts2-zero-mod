using BaseLib.Extensions;
using BaseLib.Abstracts;
using CharModTest.CharModTestCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Players;
using Godot;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Commands;

namespace CharModTest.CharModTestCode.Powers;


public class GoldEqPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    private decimal lastStrengthApplied = 0;

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        if (Owner != null && Owner.IsPlayer)
        {
            Owner.Player!.GoldChanged += OnGoldChanged;
        }

        OnGoldChanged();
        await base.AfterApplied(applier, cardSource);
    }

    public override async Task AfterRemoved(Creature oldOwner)
    {
        if (oldOwner.IsPlayer)
        {
            oldOwner.Player!.GoldChanged -= OnGoldChanged;
        }

        await base.AfterRemoved(oldOwner);
    }

    private async void OnGoldChanged()
    {
        if (Owner == null || !Owner.IsPlayer) return;
        decimal newStrength = Owner.Player!.Gold / 50;
        decimal baseValue = newStrength - lastStrengthApplied;
        lastStrengthApplied = newStrength;
        if (baseValue != 0)
            await PowerCmd.Apply<StrengthPower>(Owner, baseValue, Owner, null);
    }

    public override string CustomPackedIconPath =>
        "GoldEqPower.png".PowerImagePath();

    public override string CustomBigIconPath =>
        "GoldEqPower.png".BigPowerImagePath();
}
using BaseLib.Abstracts;
using CharModTest.CharModTestCode.Extensions;
using Godot;

namespace CharModTest.CharModTestCode.Character;

public class CharModTestPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => CharModTest.Color;
    

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}
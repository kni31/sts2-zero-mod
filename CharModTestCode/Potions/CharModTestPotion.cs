using BaseLib.Abstracts;
using BaseLib.Utils;
using CharModTest.CharModTestCode.Character;

namespace CharModTest.CharModTestCode.Potions;

[Pool(typeof(CharModTestPotionPool))]
public abstract class CharModTestPotion : CustomPotionModel;
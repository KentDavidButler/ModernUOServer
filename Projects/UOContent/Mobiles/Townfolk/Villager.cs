using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles;

[SerializationGenerator(0, false)]
public partial class Villager : BaseCreature
{
    [Constructible]
    public Villager() : base(AIType.AI_Animal, FightMode.None)
    {
        InitStats(15, 15, 15);
        SetSkill(SkillName.Begging, 36, 68);

        SetSpeed(0.2, 0.4);
        SpeechHue = Utility.RandomDyedHue();
        Title = "the villager";
        Hue = Race.Human.RandomSkinHue();

        if (Female = Utility.RandomBool())
        {
            Body = 0x191;
            Name = NameList.RandomName("female");
        }
        else
        {
            Body = 0x190;
            Name = NameList.RandomName("male");
        }

        AddItem(new Doublet(Utility.RandomDyedHue()));
        AddItem(new Boots(Utility.RandomNeutralHue()));
        AddItem(new ShortPants(Utility.RandomNeutralHue()));

        Utility.AssignRandomHair(this);

        var pack = new Backpack
        {
            Movable = false
        };

        pack.DropItem(new Gold(1, 25));
        AddItem(pack);
    }

    public override bool ClickTitle => false;
}

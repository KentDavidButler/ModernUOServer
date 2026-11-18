using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles;

[SerializationGenerator(0, false)]
public partial class HolyMen : BaseCreature
{
    [Constructible]
    public HolyMen() : base(AIType.AI_Animal, FightMode.None)
    {
        InitStats(15, 15, 15);
        SetSkill(SkillName.Healing, 36, 68);
        SetSkill(SkillName.SpiritSpeak, 36, 68);

        SetSpeed(0.2, 2.0);
        SpeechHue = Utility.RandomDyedHue();
        switch (Utility.Random(3))
        {
            case 0:
                Title = "the cleric";
                break;
            case 1:
                Title = "the priest";
                break;
            default:
                Title = "the bishop";
                break;
        }
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

        if (Utility.RandomBool())
        {
           AddItem(Utility.RandomBool() ? new QuarterStaff() : new BlackStaff());
        }

        if (Utility.RandomBool())
        {
            AddItem(new Cloak(Utility.RandomNeutralHue()));
        }

        AddItem(new Robe(Utility.RandomDyedHue()));
        AddItem(new Doublet(Utility.RandomDyedHue()));
        AddItem(new Boots(Utility.RandomNeutralHue()));
        AddItem(new ShortPants(Utility.RandomDyedHue()));

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

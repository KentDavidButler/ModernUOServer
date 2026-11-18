using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles;

[SerializationGenerator(0, false)]
public partial class Delivers : BaseCreature
{
    [Constructible]
    public Delivers() : base(AIType.AI_Animal, FightMode.None)
    {
        InitStats(15, 15, 15);
        SetSkill(SkillName.Begging, 36, 68);

        SetSpeed(0.2, 0.2);
        SpeechHue = Utility.RandomDyedHue();
        switch (Utility.Random(5))
        {
            case 0:
                Title = "the currier";
                break;
            case 1:
                Title = "the messenger";
                break;
            case 2:
                Title = "the postman";
                break;
            case 3:
                Title = "the letter carrier";
                break;
            default:
                Title = "the carter";
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
            AddItem(new LeatherGloves());
        }

        if (Utility.RandomBool())
        {
            AddItem(new Cloak(Utility.RandomNeutralHue()));
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

using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles;

[SerializationGenerator(0, false)]
public partial class Laborer : BaseCreature
{
    [Constructible]
    public Laborer() : base(AIType.AI_Animal, FightMode.None)
    {
        InitStats(15, 15, 15);
        SetSkill(SkillName.Carpentry, 13, 48);
        SetSkill(SkillName.Mining, 13, 48);
        SetSkill(SkillName.Tinkering, 13, 48);

        SpeechHue = Utility.RandomDyedHue();
        switch (Utility.Random(7))
        {
            case 0:
                Title = "the ploughman";
                break;
            case 1:
                Title = "the mason";
                break;
            case 2:
                Title = "the builder";
                break;
            case 3:
                Title = "the carpenter";
                break;
            case 4:
                Title = "the potter";
                break;
            case 5:
                Title = "the bricklayer";
                break;
            default:
                Title = "the gongfarmer";
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
            Utility.AssignRandomFacialHair(this, true);
        }

        if (Utility.RandomBool())
        {
            AddItem(new LeatherGloves());
        }

        if (Utility.RandomBool())
        {
            AddItem(new Cloak(Utility.RandomNeutralHue()));
        }

        AddItem(
            Utility.Random(2) switch
            {
                0 => new Doublet(Utility.RandomDyedHue()),
                _ => new Shirt(Utility.RandomDyedHue()) 
                }
        );
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

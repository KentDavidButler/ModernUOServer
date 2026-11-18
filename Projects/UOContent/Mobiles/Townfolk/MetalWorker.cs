using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles;

[SerializationGenerator(0, false)]
public partial class MetalWorker : BaseCreature
{
    [Constructible]
    public MetalWorker() : base(AIType.AI_Animal, FightMode.None)
    {
        InitStats(15, 15, 15);
        SetSkill(SkillName.Blacksmith, 13, 48);
        SetSkill(SkillName.Mining, 13, 48);
        SetSkill(SkillName.Tinkering, 13, 25);

        SpeechHue = Utility.RandomDyedHue();
        switch (Utility.Random(10))
        {
            case 0:
                Title = "the armorer";
                break;
            case 1:
                Title = "the blacksmith";
                break;
            case 2:
                Title = "the bellfounder";
                break;
            case 3:
                Title = "the bladesmith";
                break;
            case 4:
                Title = "the brazier";
                break;
            case 5:
                Title = "the coppersmith";
                break;
            case 6:
                Title = "the farrier";
                break;
            case 7:
                Title = "the goldsmith";
                break;
            case 8:
                Title = "the tinner";
                break;
            default:
                Title = "the cutler";
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
        AddItem(new Hatchet());

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

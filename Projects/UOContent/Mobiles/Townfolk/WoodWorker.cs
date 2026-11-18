using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles;

[SerializationGenerator(0, false)]
public partial class WoodWorker : BaseCreature
{
    [Constructible]
    public WoodWorker() : base(AIType.AI_Animal, FightMode.None)
    {
        InitStats(15, 15, 15);
        SetSkill(SkillName.Carpentry, 13, 48);
        SetSkill(SkillName.Lumberjacking, 13, 48);
        SetSkill(SkillName.Tinkering, 13, 25);

        SpeechHue = Utility.RandomDyedHue();
        switch (Utility.Random(8))
        {
            case 0:
                Title = "the carpenter";
                break;
            case 1:
                Title = "the cartwright";
                break;
            case 2:
                Title = "the harpmaker";
                break;
            case 3:
                Title = "the joiner";
                break;
            case 4:
                Title = "the lutemaker";
                break;
            case 5:
                Title = "the wheelwright";
                break;
            case 6:
                Title = "the carver";
                break;
            default:
                Title = "the woodturner";
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

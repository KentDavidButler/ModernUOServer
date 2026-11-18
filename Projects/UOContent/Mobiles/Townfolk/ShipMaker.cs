using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles;

[SerializationGenerator(0, false)]
public partial class ShipMaker : BaseCreature
{
    [Constructible]
    public ShipMaker() : base(AIType.AI_Animal, FightMode.None)
    {
        InitStats(15, 15, 15);
        SetSkill(SkillName.Carpentry, 13, 48);
        SetSkill(SkillName.Tailoring, 13, 48);
        SetSkill(SkillName.Tinkering, 13, 48);

        SpeechHue = Utility.RandomDyedHue();
        switch (Utility.Random(5))
        {
            case 0:
                Title = "the sailmaker";
                break;
            case 1:
                Title = "the shipwright";
                break;
            case 2:
                Title = "the rigger";
                break;
            case 3:
                Title = "the boatbuilder";
                break;
            default:
                Title = "the boatwright";
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
             AddItem(Utility.RandomBool() ? new SkullCap(Utility.RandomDyedHue()) : new Bandana(Utility.RandomDyedHue()));
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

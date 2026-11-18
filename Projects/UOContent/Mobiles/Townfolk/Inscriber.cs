using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles;

[SerializationGenerator(0, false)]
public partial class Inscriber : BaseCreature
{
    [Constructible]
    public Inscriber() : base(AIType.AI_Animal, FightMode.None)
    {
        InitStats(15, 15, 15);
        SetSkill(SkillName.Inscribe, 36, 68);

        SetSpeed(0.2, 2.0);
        SpeechHue = Utility.RandomDyedHue();
        switch (Utility.Random(4))
        {
            case 0:
                Title = "the scribe";
                break;
            case 1:
                Title = "the clerk";
                break;
            case 2:
                Title = "the interpreter";
                break;
            default:
                Title = "the scrivener";
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
            AddItem(new WizardsHat(Utility.RandomNeutralHue()));
        }

        if (Utility.RandomBool())
        {
            AddItem(new Cloak(Utility.RandomNeutralHue()));
        }

        if (Utility.RandomBool())
        {
            AddItem(new Robe(Utility.RandomNeutralHue()));
        }

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

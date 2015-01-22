using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace BangUIDesigner
{
    
	public enum abilities
	{
		DrawFirstCardFromOpponentHand,
		BangVSMiss,
		DropTwoCardsForLife,
		DrawCardFromClearAfterNoCards,
		DrawCardFromBatchAfterWounding,
		DrawCardFromBatchIfSecondIsRed,
		IncreasyDistance,
		DrawTwoCardsForFortune,
		DrawHeartFromBatchForMiss,
		NoBungLimit,
		SightToOpponents,
		DrawFirstCardFromClearBatch,
		CollectDeadManCards,
		DrawCardOffenderAfterWounding,
		SelectTwoCardsFromThree,
		BungNeedTwoMiss
	}

	public enum Suits
	{
		Spade,   //Pika    '©',
		Diamond, //Bubna   'Є',
		Club,    //Krest   'Ё',
		Heart,   //Cherv   '§'
	}

	public enum Roles
	{
		Sherif = 103,
		Deputy = 101,
		Renegade = 102,
		Outlaw = 100,
        None = 0
	}

	public enum CardValues
	{
		two, three, four, five, six, seven, eight, nine, ten, jack, queen, king, ace
	}

    public enum CardNames
    {
        Bung, Miss, Skofild, Volcano, Remington, Carabine,
        Winchester, Cat_Balu, Indians, Panic, Gatling,
        Coach, Jail, Duel, Beer, Shop, Mustang, Saloon,
        Walsh_Fargo, Dinamite, Barrel, Scope
    }

    public enum ObjectType
    {
        None,
        Player,
        Card,
        Table,
        Clear,
        Peal
    }

    public enum BangActions
    {
        MoveToPlayer,       //done
        MoveToTable,        //done
        MoveToClear,        //done
        MoveToMyTable,      //done
        SelectCard,         //done
        UnselectCard,       //
        TableCartToClear,   //done
        ShowCard,           //done
        Lifes,              //done
        Ligth,              //done
        HideLigth,          //done
        SetPackState,       //done
        Multiplicate,       //done
        AddCard             //done
    }

    public enum PackState
    {
        Empty,
        Low,
        Middle,
        Full
    }

    public enum CardPlace
    {
        Role,
        Hand,
        PlayerTable,
        Table
    }

    public struct CardDetailes
    {
        public int CardID;
        public CardPlace CardPlace;
        public int Tag;

        public static readonly CardDetailes Empty;

        public static bool operator ==(CardDetailes od1, CardDetailes od2)
        {
            if (od1.CardID != od2.CardID)
                return false;
            if (od1.CardPlace != od2.CardPlace)
                return false;
            if (od1.Tag != od2.Tag)
                return false;
            return true;
        }

        public static bool operator !=(CardDetailes od1, CardDetailes od2)
        {
            return !(od1 == od2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public struct PlayerDetailes
    {
        public string PlayerID;
        public Roles RoleID;
        public int Tag;

        public static readonly PlayerDetailes Empty;

        public static bool operator ==(PlayerDetailes od1, PlayerDetailes od2)
        {
            if (od1.PlayerID != od2.PlayerID)
                return false;
            if (od1.RoleID != od2.RoleID)
                return false;
            if (od1.Tag != od2.Tag)
                return false;
            return true;
        }

        public static bool operator !=(PlayerDetailes od1, PlayerDetailes od2)
        {
            return !(od1 == od2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public struct ObjectDetails
    {
        public ObjectType Type;
        public CardDetailes CardDetailes;
        public PlayerDetailes PlayerDetailes;
        public PackState PackState;
        public int Tag;

        public static readonly ObjectDetails Empty;

        public static bool operator ==(ObjectDetails od1, ObjectDetails od2)
        {
            if (od1.Type != od2.Type)
                return false;
            if (od1.CardDetailes != od2.CardDetailes)
                return false;
            if (od1.PlayerDetailes != od2.PlayerDetailes)
                return false;
            if (od1.PackState != od2.PackState)
                return false;
            if (od1.Tag != od2.Tag)
                return false;
            return true;
        }

        public static bool operator !=(ObjectDetails od1, ObjectDetails od2)
        {
            return !(od1 == od2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public struct ObjectData
    {
        public ObjectDetails SourceObject;
        public ObjectDetails DestinationObject;

        public static readonly ObjectData Empty;

//         public ObjectData GetObjectData(ObjectType type, int id)
//         {
//             Type = type;
//             ID = id;
//             return this;
//         }

        public static bool operator ==(ObjectData od1, ObjectData od2)
        {
            if (od1.SourceObject != od2.SourceObject)
                return false;
            if (od1.DestinationObject != od2.DestinationObject)
                return false;
            return true;
        }

        public static bool operator !=(ObjectData od1, ObjectData od2)
        {
            return !(od1 == od2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public enum TimerState
    {
        Ligth,
        Cards
    }

    public struct TimerData
    {
        public List<Card> cards;
        public float dx, dy;
        public float Step;
        public TimerState State;
    }

    public struct Callback
    {
        public static bool ThumbnailCallback()
        {
            return false;
        }
    }

    public enum MoveActionsType
    {
        
    }

//     public struct MoveActions
//     {
//         MoveActionsType Type;
//     }



//     public enum Roles
//     {
//         Bart_Cassidy    = "Bart_Cassidy",
//         Black_Jack      = "Black_Jack",
//         Calamity_Janet  = "Calamity_Janet",
//         El_Gringo       = "El_Gringo",
//         Jesse_Jones     = "Jesse_Jones",
//         Jourdonnais     = "Jourdonnais",
//         Kit_Carlson     = "Kit_Carlson",
//         Lucky_Duke      = "Lucky_Duke",
//         Paul_Regret     = "Paul_Regret",
//         Pedro_Ramirez   = "Pedro_Ramirez",
//         Rose_Doolan     = "Rose_Doolan",
//         Sid_Ketchum     = "Sid_Ketchum",
//         Slab_the_Killer = "Slab_the_Killer",
//         Suzy_Lafayette  = "Suzy_Lafayette",
//         Title           = "Title",
//         Vulture_Sam     = "Vulture_Sam",
//         Willy_the_Kid   = "Willy_the_Kid"
//     }
}

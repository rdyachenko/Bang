
namespace BangLogic
{
	#region Card Enums

	public enum eCardSuits
	{
		Spade,   //Pika    '©',
		Diamond, //Bubna   'Є',
		Club,    //Krest   'Ё',
		Heart,   //Cherv   '§'
	}

	public enum eCardActions
	{
		none,
		Bang,
		Miss,
		Skofild,
		Volcano,
		Remington,
		Carabine,
		Winchester,
		Cat_Balu,
		Indians,
		Panic,
		Gatling,
		Coach,
		Jail,
		Duel,
		Beer,
		Shop,
		Mustang,
		Saloon,
		WalshFargo,
		Dynamite,
		Barrel,
		Scope
	}
	public enum eCardRoles
	{
		toAny, toOpponent, toTable, toSpecial
	}

	public enum eCardValues
	{
		two, three, four, five, six, seven, eight, nine, ten, jack, queen, king, ace
	}

	#endregion

	#region Player Enums
	
	public enum eAbilities
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

	public enum ePlayerRoles
	{
		Sherif,
		Deputy,
		Renegade,
		Outlaw,
		None
	}

	#endregion

	public enum eGuns
	{
		Colt45, Volcano, Skofild, Remington, Carabine, Winchester
	}

	public enum eCardSource //звідки можна взяти карту в першій фазі ходу
	{
		batch, clear, hand, table, table_or_hand
	}

	public enum eSelectCardReasons
	{
		forMove, forDrop, forAnswer
	}

	
}

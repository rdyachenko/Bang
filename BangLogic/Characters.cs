﻿using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BangLogic
{
	class Characters: ArrayList
	{
		public Characters()
		{
			base.Add(new Character { Name = "Bart Cassidy", Ability = eAbilities.DrawCardFromBatchAfterWounding, Lifes = 4, Description = "Each time he loses a life point, he immediately draws a card from the deck." });
			base.Add(new Character { Name = "Black Jack", Ability = eAbilities.DrawCardFromBatchIfSecondIsRed, Lifes = 4, Description = "During phase 1 of his turn, he must show the second card he draws: if it’s Heart or Diamonds (just like a “draw!”), he draws one additional card (without revealing it)." });
			base.Add(new Character { Name = "Suzy Lafayette", Ability = eAbilities.DrawCardFromClearAfterNoCards, Lifes = 4, Description = "As soon as she has no cards in her hand, she draws a card from the draw pile." });
			base.Add(new Character { Name = "Kit Carlson", Ability = eAbilities.SelectTwoCardsFromThree, Lifes = 4, Description = "During phase 1 of his turn, he looks at the top three cards of the deck: he chooses 2 to draw, and puts the other one back on the top of the deck, face down." });
			base.Add(new Character { Name = "El Gringo", Ability = eAbilities.DrawCardOffenderAfterWounding, Lifes = 3, Description = "Each time he loses a  life point due to a card played by another player, he draws a random card from the hands of that player (one card for each life point). If that player has no more cards, too bad!, he does not draw. Note that Dynamite damages are not caused by any player." });
			base.Add(new Character { Name = "Pedro Ramirez", Ability = eAbilities.DrawFirstCardFromClearBatch, Lifes = 4, Description = "During phase 1 of his turn, he may choose to draw the first card from the top of the discard pile or from the deck. Then, he draws the second card from the deck." });
			base.Add(new Character { Name = "Sid Ketchum", Ability = eAbilities.DropTwoCardsForLife, Lifes = 4, Description = "At any time, he may discard 2 cards from his hand to regain one life point. If he is willing and able, he can use this ability more than once at a time. But remember: you cannot have more life points than your starting amount!" }); 
			base.Add(new Character { Name = "Rose Doolan", Ability = eAbilities.SightToOpponents, Lifes = 4, Description = "She is considered to have a Scope in play at all times; she sees the other players at a distance decreased by 1. If she has another real Scope in play, she can count both of them, reducing her distance to all other players by a total of 2" });
			base.Add(new Character { Name = "Paul Regret", Ability = eAbilities.IncreasyDistance, Lifes = 3, Description = "He is considered to have a Mustang in play at all times; all other players must add 1 to the distance to him. If he has another real Mustang in play, he can count both of them, increasing all distances to him by a total of 2." });
			base.Add(new Character { Name = "Jesse Jones", Ability = eAbilities.DrawFirstCardFromOpponentHand, Lifes = 4, Description = "During phase 1 of his turn, he may choose to draw the first card from the deck, or randomly from the hand of any other player. Then he draws the second card from the deck." });
			base.Add(new Character { Name = "Slab the Killer", Ability = eAbilities.BungNeedTwoMiss, Lifes = 4, Description = "Players trying to cancel his BANG! cards need to play 2 Missed!. The Barrel effect, if successfully used, only counts as one Missed!." }); 
			base.Add(new Character { Name = "Vulture Sam", Ability = eAbilities.CollectDeadManCards, Lifes = 4, Description = "Whenever a Character is eliminated from the game, Sam takes all the cards that player had in his hand and in play, and adds them to his hand." });
			base.Add(new Character { Name = "Willy the Kid", Ability = eAbilities.NoBungLimit, Lifes = 4, Description = "He can play any number of BANG! cards during his turn." }); 
			base.Add(new Character { Name = "Calamity Janet", Ability = eAbilities.BangVSMiss, Lifes = 4, Description = "She can use BANG! cards as Missed! cards and vice versa. If she plays a Missed! as a BANG!, she cannot play another BANG! card that turn (unless she has a Volcanic in play)." });
			base.Add(new Character { Name = "Jourdonnais", Ability = eAbilities.DrawHeartFromBatchForMiss, Lifes = 4, Description = "He is considered to have a Barrel in play at all times; he can “draw!” when he is the target of a BANG!, and on a Heart he is missed. If he has another real Barrel card in play, he can count both of them, giving him two chances to cancel the BANG! before playing a Missed!." }); 
			base.Add(new Character { Name = "Lucky Duke", Ability = eAbilities.DrawTwoCardsForFortune, Lifes = 4, Description = "Each time he is required to “draw!”, he flips the top two cards from the deck, and chooses the result he prefers. Discard both cards afterwards." });
		}
	
		public Character GetRandom()
		{
			Character ret = null;
			Random r = new Random();
			ret = (Character)base[r.Next(base.Count)];
			base.Remove(ret);
			return ret;
		}
	}
}

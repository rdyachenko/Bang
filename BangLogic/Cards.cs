using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace BangLogic
{
	public class Cards: ArrayList
	{
		private Stack<Card> _batch;
		private Stack<Card> _clear;

        private Card fromString(int cardID,string OneCardInfo)
        {
            string[] CardInfo;				// one card info
            string cardTitle = "БАХ!";						// БАХ, МИМО, etc
            string cardValue = "2";							// 2,3,4,5,6,7,8,9,10,J,Q,K,A
            eCardSuits cardColor = eCardSuits.Spade;		// Spade, Diamond, Club, Heart
            int cardDistance = 0;							// 1 to 5, 0
            eCardActions cardAction = eCardActions.Bang;	// Bang, Miss, etc
            eCardRoles cardRole = eCardRoles.toAny;			// toAny, toOpponent, toTable, toSpecial

            CardInfo = OneCardInfo.Split(' ');
            cardTitle = CardInfo[0];
            cardValue = CardInfo[1];
            switch (CardInfo[2])
            {
                case "©":
                    cardColor = eCardSuits.Spade;
                    break;
                case "Є":
                    cardColor = eCardSuits.Diamond;
                    break;
                case "Ё":
                    cardColor = eCardSuits.Club;
                    break;
                case "§":
                    cardColor = eCardSuits.Heart;
                    break;
            }
            cardAction = (eCardActions)Enum.Parse(typeof(eCardActions), CardInfo[3], true);
            cardDistance = Convert.ToInt32(CardInfo[4]);
            cardRole = (eCardRoles)Enum.Parse(typeof(eCardRoles), CardInfo[5], true);
            Card c = new Card();
            c.CardID = cardID;
            c.CardAction = cardAction;
            c.CardColor = cardColor;
            c.CardDistance = cardDistance;
            c.CardRole = cardRole;
            c.CardTitle = cardTitle;
            c.CardValue = cardValue;
            return c;
        }
		public Cards(string SkinFolder)
		{
			_batch = new Stack<Card>();
			_clear = new Stack<Card>();
			
			string[] AllCardsInfo;			// all cards in file

			if ((!string.IsNullOrEmpty(SkinFolder)) && Directory.Exists(Application.StartupPath + "\\" + SkinFolder))
				AllCardsInfo = File.ReadAllLines(Application.StartupPath + "\\" + SkinFolder + "\\cardlist.ini");
			else
				AllCardsInfo = File.ReadAllLines(Application.StartupPath + "\\Default\\cardlist.ini");

            int cardID = 0;									// card ID
			foreach (string OneCardInfo in AllCardsInfo)
			{

                Card c = fromString(cardID++, OneCardInfo);
				base.Add(c);
			}
		}

		public void Shuffle()
		{
			Card spool;
			Random rnd = new Random();
			for (int i = 0;i < base.Count;i++)
			{
				int tmp = rnd.Next(base.Count);
				spool = (Card)base[tmp];
				base[tmp] = base[i];
				base[i] = spool;
			}
			for (int j = 0;j < 80;j++)
				_batch.Push((Card)base[j]);
			_clear.Clear();
		}

		private Card GetRandom()
		{
			Card ret = null;
			Random r = new Random();
			ret = (Card)base[r.Next(base.Count)];
			base.Remove(ret);
			return ret;
		}

        public Card CardByID(int id)
        {
            string[] AllCardsInfo = File.ReadAllLines(Application.StartupPath + "\\Default\\cardlist.ini");
            return (Card)fromString(id,AllCardsInfo[id]);
        }
        
        public Card GetFromCardBatch()
        {
            Card forRet = new Card();
            if (_batch.Count == 0) Shuffle();
            forRet = _batch.Pop();
            return forRet;
        }

		public Card[] GetFromCardBatch(int count)
		{
			Card[] forRet = new Card[count];
			for (int i = 0;i < count;i++)
			{
				if (_batch.Count == 0) Shuffle();
				forRet[i] = _batch.Pop();
			}
			return forRet;
		}

		public Card GetFromClearBatch()
		{
			Card forRet = null;
			if (_clear.Count > 0)
			{
				forRet = _clear.Pop();
			}
			return forRet;
		}

		public void PutToCardBatch(Card[] Cards)
		{
			for (int i = 0;i < Cards.Length;i++)
				_batch.Push(Cards[i]);
		}

		public void PutToClearBatch(Card[] Cards)
		{
			for (int i = 0;i < Cards.Length;i++)
				_clear.Push(Cards[i]);
		}

	}
}

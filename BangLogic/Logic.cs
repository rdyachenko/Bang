using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BangLogic
{
    public class Logic
    {
        #region Constants
        private const Int32 CardCount = 81;
        public Cards _cardBatch;
        #endregion Constants
        #region Variables
        private Players _players;
        private Characters _characters;
        private Roles _roles;
        private Player _localPlayer;
        #endregion Variables

        public Logic()
        {
            // Open cards from "box"
            _cardBatch = new Cards("Default");
            // Shuffle batch
            _cardBatch.Shuffle();
            // players container
            _players = new Players();
            // Load characters
            _characters = new Characters();
            // Load roles
            _roles = new Roles();
        }

        public void AddPlayer(Player player)
        {
            // add random person
            if (player.Character == null)
            {
                Character person = _characters.GetRandom();
                player.Character = person;
            }
            else
            {
                _characters.Remove(player.Character);
            }
            // Add player to local collection
            _players.Add(player);
            if (player.isLocal()) _localPlayer = player;
        }

        public void RemovePlayer(string playerName)
        {
            Player pl = _players.PlayerByName(playerName);
            _characters.Add(pl.Character);
            _players.Remove(pl);
            if (pl.isLocal()) _localPlayer = null;
        }

        public void StartGame()
        {
            // Assign roles and put cards
            Roles rls = new Roles(_players.Count);
            foreach (Player player in _players)
            {
                player.Role = rls.GetRandom();
                player.PutCardsToHand(_cardBatch.GetFromCardBatch((player.Role == ePlayerRoles.Sherif)?  player.Lifes+1:  player.Lifes));
            }
        }

        public int PlayersCount()
        {
            return _players.Count;
        }

        public Player LocalPlayer()
        {
            return _localPlayer;
        }       

        public Player GetPlayerByIP(string IP)
        {
            foreach (Player p in _players)
            {
                if (p.IP == IP)
                    return p;
            }
            return null;
        }
        
        public bool PlayerExist(string playerName)
        {
            Player p = _players.PlayerByName(playerName);
            return p != null;
        }

        public string ToXML()
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendFormat("\r<players count=\"{0}\">", _players.Count);
            foreach (Player i in this._players) { xml.Append(i.ToXML()); }
            xml.Append("\r</players>");
            return xml.ToString();
        }

        public bool clickOnPealIsValid(string playerID)
        {
            return true;
        }
        public bool clickOnClearIsValid(string playerID)
        {
            return true;
        }
        public bool clickOnCardIsValid(string playerID, int cardID)
        {
            return true;
        }
        public bool clickOnPlayerIsValid(string playerID, string targetPlayerID)
        {
            return true;
        }
        public bool clickOnTableIsValid(string playerID)
        {
            return true;
        }

        public int clickOnPeal(string playerID)
        {
            Card[] cc = new Card[1];
            cc[0] =  _cardBatch.GetFromCardBatch();
            Player p = _players.PlayerByName(playerID);
            p.PutCardsToHand(cc);
            return cc[0].CardID;
        }
        public int clickOnClear(string playerID)
        {
            Card c = _cardBatch.GetFromClearBatch();
            return c.CardID;
        }
        public void clickOnCard(string playerID, int cardID)
        {
        }
        public void clickOnPlayer(string playerID, string targetPlayerID)
        {   
        }
        public void clickOnTable(string playerID)
        {
        }

        public string[] PlaersIPs()
        {
            string[] ips = new string[_players.Count];
            int i=-1;
            foreach (Player p in _players)
            {
                ips[++i] = p.IP;
            }
            return ips;
        }
    }
}

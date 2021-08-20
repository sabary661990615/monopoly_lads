﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Messaging;

namespace mpy
{
    /// <summary>
    /// The Form that shows the Monopyly board.
    /// </summary>
    public partial class Monopyly : Form
    {
        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Monopyly()
        {
            InitializeComponent();
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Called when the form is loaded.
        /// </summary>
        private void Monopyly_Load(object sender, EventArgs e)
        {
            // We start the messaging-client...
            m_messagingClient = new MessagingClient();
            m_messagingClient.StartOfTournamentEvent += onStartOfTournament;
            m_messagingClient.StartOfGameEvent += onStartOfGame;
            m_messagingClient.BoardUpdateEvent += onBoardUpdate;
            m_messagingClient.PlayerInfoEvent += onPlayerInfo;
        }

        /// <summary>
        /// Called when the tournament starts.
        /// </summary>
        private void onStartOfTournament(object sender, MessagingClient.StartOfTournamentArgs e)
        {
            ctrlBoard.SetPlayers(from p in e.StartOfTournament.player_infos select p.player_name);
        }

        /// <summary>
        /// Called at the start of a new game.
        /// </summary>
        private void onStartOfGame(object sender, EventArgs e)
        {
            ctrlBoard.StartOfGame();
        }

        /// <summary>
        /// Called when we receive a player-info update.
        /// </summary>
        private void onPlayerInfo(object sender, MessagingClient.PlayerInfoArgs e)
        {
            foreach(var playerInfo in e.PlayerInfo.player_infos)
            {
                ctrlBoard.UpdatePlayerInfo(
                    playerInfo.player_number,
                    playerInfo.net_worth,
                    playerInfo.games_won,
                    playerInfo.square,
                    playerInfo.ms_per_turn);
            }
        }

        /// <summary>
        /// Called when we get a board update.
        /// </summary>
        private void onBoardUpdate(object sender, MessagingClient.BoardUpdateArgs e)
        {
            ctrlBoard.BoardUpdate = e.BoardUpdate;
        }

        /// <summary>
        /// Called just before the form closes.
        /// </summary>
        private void Monopyly_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_messagingClient.Dispose();
        }

        /// <summary>
        /// Called when the timer ticks.
        /// </summary>
        private void ctrlTimer_Tick(object sender, EventArgs e)
        {
            // We update the board with the latest game information...
            ctrlBoard.Invalidate();
        }

        #endregion

        #region Private data

        // The messaging client...
        private MessagingClient m_messagingClient = null;

        #endregion

    }
}

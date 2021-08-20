﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace mpy
{
    /// <summary>
    /// An base class for squares on the board.
    /// </summary>
    abstract class Square
    {
        #region Public methods and properties

        /// <summary>
        /// Static constructor.
        /// </summary>
        static Square()
        {
            // We load the houses and hotels...
            HouseHorizontal = Utils.loadBitmap("graphics/house_horizontal.png");
            HouseVertical = Utils.loadBitmap("graphics/house_vertical.png");
            HotelHorizontal = Utils.loadBitmap("graphics/hotel_horizontal.png");
            HotelVertical = Utils.loadBitmap("graphics/hotel_vertical.png");
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Square()
        {
            Clear();
        }

        /// <summary>
        /// The Y-position on the board of the top of the square.
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// The Y-position on the board of the bottom of the square.
        /// </summary>
        public int Bottom { get; set; }

        /// <summary>
        /// The X-position on the board of the left of the square.
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// The X-position on the board of the right of the square.
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// Clears all information (in particular about which players are
        /// on the square).
        /// </summary>
        public void Clear()
        {
            NumberOfPlayersOnSquare = 0;
        }

        /// <summary>
        /// An offset used for the player position if there is
        /// more than one players on the square.
        /// </summary>
        public Point PlayerOffset 
        {
            get
            {
                switch(NumberOfPlayersOnSquare)
                {
                    case 2:
                        return new Point(10, 10);
                    case 3:
                        return new Point(-10, -10);
                    case 4:
                        return new Point(-10, 10);
                    default:
                        return new Point(0, 0);
                }
            }
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Shows the square as mortgaged.
        /// </summary>
        public abstract void ShowMortgaged(Graphics g);

        /// <summary>
        /// Shows the owner of the square.
        /// </summary>
        public abstract void ShowOwner(Graphics g, Bitmap ownerShape);

        /// <summary>
        /// Shows houses or hotels.
        /// </summary>
        public abstract void ShowHouses(Graphics g, int numberOfHouses);

        /// <summary>
        /// Shows the player token on the square.
        /// </summary>
        public abstract void ShowPlayer(Graphics g, Bitmap playerShape, bool inJail);

        #endregion

        #region Protected methods and properties

        /// <summary>
        /// House for use on the top and bottom sides of the board.
        /// </summary>
        protected static Bitmap HouseHorizontal { get; set; }

        /// <summary>
        /// House for use on the left and right sides of the board.
        /// </summary>
        protected static Bitmap HouseVertical { get; set; }

        /// <summary>
        /// Hotel for use on the top and bottom sides of the board.
        /// </summary>
        protected static Bitmap HotelHorizontal { get; set; }

        /// <summary>
        /// Hotel for use on the left and right sides of the board.
        /// </summary>
        protected static Bitmap HotelVertical { get; set; }

        /// <summary>
        /// The number of players on the square.
        /// </summary>
        protected int NumberOfPlayersOnSquare { get; set; }

        /// <summary>
        /// The width of the mortgage line.
        /// </summary>
        protected const int MORTGAGE_LINE_WIDTH = 6;

        #endregion

    }
}

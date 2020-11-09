using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{   
    /// <summary>
    /// this is the value of each cell is currently at
    /// </summary>
    public enum MarkType
    {
        /// <summary>
        /// The cell Hasnt been clicked 
        /// </summary>
        Free,
        /// <summary>
        /// the cell value is 0
        /// </summary>
        Nought,
        /// <summary>
        /// the cell value is x
        /// </summary>
        Cross
    }
}

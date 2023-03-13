using System;
using System.ComponentModel.DataAnnotations;


namespace Module101TicTacToeApp.Models
{
    public class TicTacToe
    {
        public string? result { get; set; }

        public string? currentTurn { get; set; }

        public string? currentTurnLabel { get; set; }

        public List<string>? blocks { get; set; }

    }
}

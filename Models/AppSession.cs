using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


//Shivam Parekh

namespace Module101TicTacToeApp.Models
{
    public class AppSession
    {
        private const string BlocksKey = "blocks";
        private const string CurrentTurnKey = "currentturn";
        private ISession session { get; set; }

        public AppSession(ISession session)
        {
            this.session = session;
        }

        public void SetBlocks(List<string> blocks)
        {
            session.SetString(BlocksKey, JsonConvert.SerializeObject(blocks));
        }
        public List<string>? GetBlocks() {
            var value = session.GetString(BlocksKey);
            return (string.IsNullOrEmpty(value)) ? default :
                JsonConvert.DeserializeObject<List<string>>(value);
        }

        public void SetCurrentTurn(string value) { 
            session.SetString(CurrentTurnKey, value);
        }

        public string? GetCurrentTurn()
        {
            var value = session.GetString(CurrentTurnKey);
            return (string.IsNullOrEmpty(value)) ? default : value;
        }
        public void ClearSession()
        {
            session.Remove(BlocksKey);
            session.Remove(CurrentTurnKey);
        }
    }
}

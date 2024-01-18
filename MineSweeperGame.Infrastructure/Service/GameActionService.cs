using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperGame.Infrastructure.Service
{
    public class GameActionService
    {
        public GameActionService() { }

        public int perfomeReveal(string blockNum,List<string> minesCorditnate,int gridSize)
        {
            int revealNum = 0;
            string[] split = blockNum.Split();

            for(int i = 0; i < gridSize; i++)
            {
               
            }

            revealNum = -1;

            return revealNum;
        }

        private List<string> getAdjacentBlocks(string blockNum,int gridSize)
        {
            List<string> list = new List<string>();
            string[] split = blockNum.Split();
           
            return list;
        }
    }
}

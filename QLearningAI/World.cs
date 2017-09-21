using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLearningAI
{
    class World
    {
        private int[] ActionSpace { get; set; }
        private int N_Actions = 0;

        //from env
        private int Speed { get; set; }
        private int Balls { get; set; }
        private double Playtime { get; set; }

        //private int PlayerScore { get; set; }

        //private int[] S, S_;

        public World(int[] actions)
        {
            ActionSpace = actions;
            N_Actions = ActionSpace.Length;
        }

        public int Get_N_Actions()
        {
            return N_Actions;
        }

        public void BuildInitialObservation()
        {
            //get speed from unity
            //get balls from unity
            //get time/score from unity
        }

        public object[] Reset()
        {

            Speed = 0;  //reset from stored/orig
            Balls = 0;  // reset from stored/orig

            int speed = Speed;
            int balls = Balls;
            double playtime = Playtime;

            object[] state = { speed, balls, playtime };

            return state;
        }

        public bool Step(int action, out object[] S_, out int reward)
        {
            //get player score, score must be within (20, 50) for example
            bool done = false;

            if(action == 0) //increase speed
            {
                Speed += 10;
            }else if(action == 1) // decrease speed
            {
                Speed -= 10;
            }else if(action == 2) // increase balls
            {
                Balls += 1;
            }else if(action == 3) // decrease balls
            {
                Balls -= 1;
            }

            //What defines the NEXT STATE is the ball's new speed and number
            //THIS IS THE NEXT STATE S_
            int speed = Speed;
            int balls = Balls;
            double playtime = Playtime;

            S_ = new object[]{ speed, balls, playtime };

            if (Enumerable.Range(50, 120).Contains(Convert.ToInt32(S_[2])))
            {
                reward = 1;
                done = true;
            }
            else
            {
                reward = 0;
                done = false;
            }
            return done;
        }



    }
}

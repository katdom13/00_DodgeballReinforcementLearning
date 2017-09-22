using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLearningAI
{
    class RL_Brain
    {
        private int[] Actions { get; set; }
        private double Alpha { get; set; } //learning rate
        private double Gamma { get; set;  } //reward decay
        private double Epsilon { get; set; } //greedy attribute
        private Dictionary<int[], double[]> Q_Table { get; set; }

        

        public RL_Brain(int[] actions)
        {
            Alpha = 0.1;
            Gamma = 0.9;
            Epsilon = 0.9;

            Actions = actions;

            Q_Table = new Dictionary<int[], double[]>();

        }
        
        private Random rand = new Random();

        public int ChooseAction(int[] speed_ball_state)
        {
            int A = 0;

            CheckIfStateExists(speed_ball_state);

            if (rand.NextDouble() < Epsilon)
            {
                double[] state_actions = Q_Table[speed_ball_state];
                Shuffle(state_actions);

                //max action
                var maxAction = state_actions.Max();
                var indexOfMax = Array.IndexOf(state_actions, maxAction);

                A = Actions[indexOfMax];

            }
            else
            {
                A = Actions[rand.Next(Actions.Length)];
            }

            return A;

        }

        public void Learn(int[] S, int A, int R, int[] S_)
        {

            double q_target = 0.0;

            CheckIfStateExists(S_);

            double q_predict = Q_Table[S][A];

            int[] terminal = new int[]{ -1, -1 };

            if (S.Equals(terminal))
            {
                q_target = R + Gamma * Q_Table[S_].Max();
            }
            else
            {
                q_target = R;
            }

            Q_Table[S][A] += Alpha * (q_target - q_predict);

        }

        public void CheckIfStateExists(int[] speed_ball)
        {
            if (Q_Table.ContainsKey(speed_ball) == false)
            {
                Q_Table.Add(speed_ball, new double[Actions.Length]);
            }
        }

        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);

                T value = list[k];
                int key = Actions[k];

                list[k] = list[n];
                Actions[k] = Actions[n];

                list[n] = value;
                Actions[n] = key;
            }
        }

    }
}

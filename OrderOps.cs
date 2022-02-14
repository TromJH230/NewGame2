using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewGame1
{
    public class OrderOps
    {
        List<Operation> opList;
        Operation current;
        public OrderOps()
        {
            opList = new List<Operation>();
        }

        public void addOp(List<string> ms, int wait)
        {
            Operation newOp = new Operation(ms, wait);
            opList.Add(newOp);
            if (current == null)
            {
                current = newOp;
            }
        }

        public void addOp(Button b, int wait)
        {
            Operation newOp = new Operation(b,wait);
            opList.Add(newOp);
        }

        public void addOp(Button b, int wait, Action act)
        {
            Operation newOp = new Operation(b, wait, act);
            opList.Add(newOp);
        }
        public dynamic getNext()
        {
            if (opList.Count == 0)
            {
                current = null;
                return null;
            }
            current = opList[0];
            opList.RemoveAt(0);
            //Tuple<T, int> toReturn = new Tuple<T, int>(current.getContent(T.GetType()), current.getWaitTime());
            return current.getContent();
        }

        public dynamic peekNextObject()
        {
            return current.getContent().Item1;
        }

        public dynamic peekNextWait()
        {
            return current.getContent().Item2;
        }

        public bool hasNext()
        {
            return opList.Count != 0;
        }
        public Type getType()
        {
            return current.type;
        }

        private class Operation
        {
            private List<string> messages;
            private Button button;
            private int waitTime;
            public Type type;
            private Action action;

            public Operation(List<string> ms, int wait)
            {
                messages = ms;
                type = ms.GetType();
                waitTime = wait;
            }

            public Operation(Button b, int wait)
            {
                button = b;
                type = b.GetType();
                waitTime = wait;
            }

            public Operation(Button b, int wait, Action act)
            {
                button = b;
                type = b.GetType();
                waitTime = wait;
                action = act;
            }

            public Operation(List<string> ms, Button b, int wait)
            {
                messages = ms;
                button = b;
                waitTime = wait;
            }
            
            public int getWaitTime()
            {
                return waitTime;
            }
            public dynamic getContent<T>()
            {
                if (typeof(T) == typeof(List<string>))
                {
                    Console.WriteLine("Here5");
                    return messages;
                } else if (typeof(T) == typeof(Button))
                {
                    return button;
                }
                return null;
            }

            public dynamic getContent()
            {
                if(type == typeof(List<string>))
                {
                    if (action != null)
                    {
                        return new Tuple<List<string>, int, Action>(messages, waitTime, action);
                    }
                    Tuple<List<string>, int> toReturn = new Tuple<List<string>, int>(messages, waitTime);
                    return toReturn;
                } else if (type == typeof(Button))
                {
                    if (action != null)
                    {
                        return new Tuple<Button, int, Action>(button, waitTime, action);
                    }
                    Tuple<Button, int> toReturn = new Tuple<Button, int>(button, waitTime);
                    return toReturn;
                }
                return null;
            }
        }
    }
}

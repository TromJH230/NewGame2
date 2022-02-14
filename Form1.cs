using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewGame1
{
    public partial class Form1 : Form
    {
        private Room current;
        private List<Action> actions;
        private int borderWidth = 10;
        private OrderOps orderOp;
        private List<Tuple<Button, Action>> hidden;
        Player player;
        public Form1(Room c)
        {
            current = c;
            player = current.gm.player;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateForm(current);
            panelSetup();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.Black, borderWidth);
            g.DrawRectangle(pen, p.DisplayRectangle);
            pen.Dispose();
            g.Dispose();
        }
        private void mapPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.Black, borderWidth);
            g.DrawRectangle(pen, p.DisplayRectangle);
            /*
            Font font = new Font(FontFamily.GenericMonospace, 16);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString("Testing", font, brush, p.Location.X, p.Location.Y);
            */
            pen.Dispose();
            g.Dispose();
            //font.Dispose();
            //brush.Dispose();
        }

        private void MessageOut_Paint(object sender, PaintEventArgs e)
        {
            MessageOut.BorderStyle = BorderStyle.Fixed3D;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "Search Room")
            {
                Control control = actionPanel.Controls[0];
                actionPanel.Controls.Remove(control);
                control.Dispose();
                current.doSearch();
                updateMap();
                updateRoomView();
                timer1.Enabled = true;
            } 
            else
            {
                Action a = actions[(int)button.Tag - 1];
                if (button.Text.Contains("Collect"))
                {
                    removeButton(button); //Removes Button
                    current.removeContent(a.amount, a.item); //Removes collected item from room

                    {
                        string newMessage = MessageOut.Text;
                        string toReplace = a.amount + " " + a.item.itemName + "\n";
                        if (current.contents.Count == 0)
                        {
                            newMessage = newMessage.Replace(toReplace, "seemingly nothing...\n");
                        }
                        else
                        {
                            newMessage = newMessage.Replace(toReplace, "");
                        }
                        newMessage += "Collected " + a.amount + " " + a.item.itemName + "\n";
                        MessageOut.Text = newMessage;
                    } //Removes item from the MessageOut text

                    player.addToInventory(a.amount, a.item); //Adding item(s) to player inventory
                    inventoryText.Text = player.getInventoryMessage(); //Updates player inventory panel message
                } //Collect button pressed
                else if (button.Text.Contains("The Furnace")) 
                {
                    Action b = actions[(int)button.Tag - 1];
                    if (button.Text.Contains("Fuel"))
                    {
                        if (player.getAmt(new Item("Gas")) > 0)
                        {
                            player.addToInventory(-1, new Item("Gas"));
                        }
                        if (b.item.furnaceFuel > 0)
                        {
                            MessageOut.Text += "You added more fuel\n";
                        }
                        else
                        {
                            MessageOut.Text += "The Furnace is now fueled\n";
                        }
                        inventoryText.Text = player.getInventoryMessage();
                        b.item.furnaceFuel += 100;
                        if (player.getAmt(new Item("Gas")) <= 0) removeButton(button);
                    }
                    else if (button.Text.Contains("Light") || button.Text.Contains("Extinguish"))
                    {
                        b.item.isLit = !b.item.isLit;

                        Graphics g = CreateGraphics();
                        string text = b.getAction();
                        button.SetBounds(button.Location.X, button.Location.Y, (int)g.MeasureString(text, button.Font).Width + 10, button.Height);
                        button.Text = text;
                        g.Dispose();

                        //current.rArt = current.gm.art.buildArt(current);
                        if (b.item.isLit)
                        {
                            MessageOut.Text += "You lit The Furnace\n";
                            updateRoomView();
                        }
                        else
                        {
                            MessageOut.Text += "You extinguished The Furnace\n";
                            updateRoomView();
                        }
                    }
                } //Fuel Furnace button pressed
                else
                {
                    timer1.Interval = 10;
                    timer1.Enabled = false;
                    this.timer1.Tick -= this.timer1_Tick;
                    Action act = actions[(int)button.Tag - 1];
                    actionPanel.Controls.Clear();
                    current.cleanActions();
                    if (current.gm.furnace.isLit)
                    {
                        current.gm.furnace.furnaceFuel -= 5;
                    }
                    updateForm(act.getRoom());
                } //Move Room button pressed
            }
            showHidden();
        }

        private void panelSetup()
        {
            roomView.Location = new Point(borderWidth / 2, borderWidth / 2);
            MessageOut.Location = new Point(panel1.Location.X, panel1.Location.Y + panel1.Height + borderWidth + 5);
            actionPanel.Location = new Point(panel1.Location.X + panel1.Width - borderWidth - actionPanel.Width, MessageOut.Location.Y);
            mapPanel.Location = new Point(panel1.Location.X + panel1.Width - (borderWidth / 2), panel1.Location.Y);
            mapText.Location = new Point(borderWidth / 2, borderWidth / 2);
            inventoryPanel.Location = new Point(mapPanel.Location.X, mapPanel.Location.Y + mapPanel.Height - (borderWidth / 2));
            inventoryText.Location = new Point(borderWidth / 2, borderWidth / 2);
        }

        private void updateForm (Room r)
        {
            current = r;
            player = current.gm.player;
            hidden = new List<Tuple<Button, Action>>();

            //MessageOut
            MessageOut.Text = "";
            {
                {
                    /*
                    Font font = new Font(FontFamily.GenericMonospace, 16);
                    int width = mapPanel.Width - borderWidth;
                    string fillMap = "#";
                    Graphics g = CreateGraphics();
                    while (true)
                    {
                        string temp = fillMap;
                        temp += " #";
                        if (width <= g.MeasureString(temp, font).Width)
                        {
                            break;
                        }
                        else
                        {
                            fillMap += " #";
                        }
                    }
                    fillMap += "\n";
                    string hold = fillMap;
                    while (true)
                    {
                        string temp = hold;
                        temp += fillMap;
                        if (mapPanel.Height - borderWidth <= g.MeasureString(temp, font).Height)
                        {
                            fillMap = hold;
                            break;
                        }
                        else
                        {
                            hold += fillMap;
                        }
                    }
                    mapText.Font = font;
                    mapText.Text = fillMap;
                    Console.WriteLine("W: " + g.MeasureString(fillMap, font).Width + ", " + g.MeasureString(fillMap, font).Height);
                    Console.WriteLine(fillMap);
                    */
                } //Measuring the MapBox
                updateMap();
            } //MapText
            //mapText.Text = current.gm.visualMap;

            inventoryText.Text = player.getInventoryMessage();
            orderOp = new OrderOps();
            populateOp();
            updateRoomView();

            timer1.Interval = 10;
            timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

            listActions.Items.Clear();
            listActions.BeginUpdate();
            foreach (Action action in actions)
            {
                listActions.Items.Add(action.getAction());
            }
            listActions.EndUpdate();
        }

        public void populateOp()
        {
            List<Tuple<List<string>, int>> order = current.getOrder();
            actionPanel.Controls.Clear();
            actions = current.getActions();
            foreach (Tuple<List<string>, int> combo in order)
            {
                List<string> line = combo.Item1;
                if (line[0].Contains("Search Button"))
                {
                    Graphics g = CreateGraphics();
                    Button button = new Button();
                    button.Tag = 0;
                    string text = "Search Room";
                    button.SetBounds(button.Location.X, button.Location.Y, (int)g.MeasureString(text, button.Font).Width + 10, button.Height);
                    button.Text = text;
                    button.Text = "Search Room";
                    button.Click += button_Click;
                    actionPanel.Controls.Add(button);
                    button.Hide();
                    orderOp.addOp(button, combo.Item2);
                    g.Dispose();
                }
                else if (line[0] == "insert_Actions")
                {
                    for (int j = 0; j < actions.Count; j++)
                    {
                        Graphics g = CreateGraphics();
                        Button button = new Button();
                        button.Tag = j + 1;
                        string text = actions[j].getAction();
                        button.SetBounds(button.Location.X, button.Location.Y, (int)g.MeasureString(text, button.Font).Width + 10, button.Height);
                        button.Text = text;
                        button.Click += button_Click;
                        actionPanel.Controls.Add(button);
                        button.Hide();
                        g.Dispose();
                        if (j == actions.Count - 1) 
                        {
                            if (actions[j].amount == -1)
                            {
                                orderOp.addOp(button, 5, actions[j]);
                            } else
                            {
                                orderOp.addOp(button, combo.Item2);
                            }
                        } //If last action in list make timer pause
                        else
                        {
                            if (actions[j].amount == -1)
                            {
                                orderOp.addOp(button, 5, actions[j]);
                            }
                            else
                            {
                                orderOp.addOp(button, 5);
                            }
                        } //Else show next action quickly
                    }
                }
                else
                {
                    orderOp.addOp(line, combo.Item2);
                }
            }
        }

        private void updateMap()
        {
            Font font = new Font(FontFamily.GenericMonospace, 16);
            string text = "";
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    Room room = current.gm.map[i, j];
                    if (i != 0)
                    {
                        text += " ";
                    }
                    if (room != null)
                    {
                        if (room.isSearched())
                        {
                            if (room == current)
                            {
                                text += "X";
                            }
                            else
                            {
                                text += "E";
                            }
                        }
                        else if (room.noticed && room != current)
                        {
                            text += "0";
                        }
                        else
                        {
                            if (room == current)
                            {
                                text += "X";
                            }
                            else
                            {
                                text += "#";
                            }
                        }
                    }
                    else
                    {
                        text += "#";
                    }
                }
                text += "\n";
            }
            mapText.Font = font;
            mapText.Text = text;
            font.Dispose();
        }

        private void updateRoomView()
        {
            Font font = new Font(FontFamily.GenericMonospace, 8);
            roomView.Font = font;
            if (current.isSearched())
            {
                current.rArt = current.gm.art.buildArt(current);
                roomView.Text = current.rArt;
            }
            else
            {
                roomView.Text = current.gm.art.unsearchedRoom;
            }
            font.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            var tuple = orderOp.getNext();
            if (tuple != null)
            {
                if (tuple.Item1.GetType() == typeof(Button))
                {
                    Button button = tuple.Item1;
                    if (tuple.GetType() == typeof(Tuple<Button, int, Action>))
                    {
                        Action act = tuple.Item3;
                        if (act.findCanShow())
                        {
                            button.Show();
                        } else
                        {
                            button.Hide();
                            hidden.Add(new Tuple<Button, Action>(button, act));
                        }
                    } else
                    {
                        button.Show();
                    }
                }
                else if (tuple.Item1.GetType() == typeof(List<string>))
                {
                    Graphics g = CreateGraphics();
                    foreach (string line in tuple.Item1)
                    {
                        MessageOut.Text = MessageOut.Text + line;
                        MessageOut.Width = (int)g.MeasureString(MessageOut.Text.ToString(), MessageOut.Font).Width + 1;
                    }
                }
                if (tuple.Item2 != -1)
                {
                    timer1.Interval = tuple.Item2;
                    timer1.Enabled = true;
                } else
                {
                    timer1.Interval = 10;
                }
            } 
            /*
            else
            {
                this.timer1.Tick -= this.timer1_Tick;
            }
            */
        }
        private void showHidden()
        {
            foreach (Tuple<Button, Action> tup in hidden)
            {
                if (tup.Item2.findCanShow()) tup.Item1.Show();
            }
        }

        private void removeButton(Button button)
        {
            foreach (Control control in actionPanel.Controls)
            {
                if (control.Tag == button.Tag)
                {
                    actionPanel.Controls.Remove(control);
                }
            }
        }
    }
}

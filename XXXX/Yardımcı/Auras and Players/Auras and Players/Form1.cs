using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Auras_and_Players
{
    public class RevCam
    {
        public RevCam() { }
        public bool inUse = false;
        public bool bright = false;
        public bool contra = false;
    }

    public partial class Form1 : Form
    {
        Player p;
        Aura a;
        
        Dictionary<string, Actor> Actors = new Dictionary<string, Actor>();
        
        AuraHandler auraHandler;

        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p = new Player("Leeras", new Point(0, 0), new Dictionary<Item, int>(), Alignment.good);
            a = new Aura(3, Stat.agility, 10, "Agile!", p);
            p.actorStats[Stat.agility] += 10;
            
            //p.AddAura(a);
            Actors.Add("Player", p);
            auraHandler = new AuraHandler(Actors);
            auraHandler.AddAura(a, "Player");
            a.buffRanOut += a_buffRanOut;
        }

        void a_buffRanOut(object sender, EventArgs e)
        {
            debug();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            debug();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (p.auras.Contains(a))
            {
                p.DispellAura(a);
            }
            else
            {
                p.AddAura(a);
            }
            debug();
        }

        void debug() {
            string stattext = "", auratext = "";
            foreach (Stat item in p.actorStats.Keys)
            {
                stattext += item + "=" + p.actorStats[item].ToString() + "\n";
            }
            richTextBox1.Text = stattext;
            foreach (Aura item in p.auras)
            {
                auratext = item.Name + "(" + item.seconds.ToString() + ")\n";
            }
            richTextBox2.Text = auratext;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace ArbitesEto
{
    public partial class FmKeySelector
    {
        public FmKeySelector()
        {
            InitializeComponent();
            Icon = new Icon(MdConstants.icon);
            AddTabs(ClKey.lists);
        }


        private void AddTabs(List<ClKeyGroup> cg)
        {

            foreach (ClKeyGroup kg in ClKey.lists)
            {
                /*del
                var kng = new ClNKeyGroup(kg.name, kg.priority, new List<ClNKey>());
                //*/
                var tp = new TabPage();
                tp.Text = kg.name;
                TCMain.Pages.Add(tp);

                var sc = new Scrollable();
                sc.Border = BorderType.None;
                tp.Content = sc;

                var tb = new TableLayout();
                tb.Rows.Add(new TableRow());
                sc.Content = tb;

                const int MAX = 8;
                var slList = new List<StackLayout>();


                int counter = 0;
                for (int i = 0; i < MAX; i ++ )
                {
                    tb.Rows[0].Cells.Add(new TableCell());
                    var sl = new StackLayout();
                    slList.Add(sl);
                    tb.Rows[0].Cells[i].Control = sl;
                }

                foreach (ClKey key in kg.key)
                {
                    /*del
                    var clnk = new ClNKey(key.val, key.ktype, key.display, false);
                    if (clnk.display.Contains("Fn") || clnk.display.Contains("Layer"))
                    {
                        clnk.allLayers = true;
                    }
                    kng.key.Add(clnk);
                    //*/



                    var newb = new Button();
                    newb.Tag = "bt_" + key.val + "_" + key.ktype;
                    newb.Text = key.display;
                    newb.Size = new Size(72, 72);
                    newb.Click += (sender, e) => KeyBtnClicked(sender, e);
                    slList[counter].Items.Add(newb);
                    counter++;
                    if (counter >= MAX)
                    {
                        counter = 0;
                    }
                }

                /*
                System.IO.Directory.CreateDirectory("keygroups2");
                MdCore.Serialize<ClNKeyGroup>(kng, "keygroups2" + MdConstants.pseparator + kng.name.ToLower() + MdConstants.eKeygroups.Substring(1));
                //*/
            }
        }

        public void KeyBtnClicked(object sender, EventArgs e)
        {
            string btn = (sender as Button).Tag.ToString();
            btn = btn.Substring(btn.IndexOf("_") + 1);
            int val = Convert.ToByte(btn.Substring(0, btn.IndexOf("_")));
            btn = btn.Substring(btn.IndexOf("_") + 1);
            int ktype = Convert.ToByte(btn);
            MdGlobals.selectedKey = ClKey.dKeys.Find(k => (k.val == val && k.ktype == ktype));
            MdGlobals.selectedSpecial = true;
        }





    }
}

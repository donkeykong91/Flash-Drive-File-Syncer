﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ManageDevices
{
    public partial class Form2 : Form
    {
        Main form1;
        public Form2(Main form_1)
        {
            InitializeComponent();
            form1 = form_1;
        }

        // For the 'No' button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //For the 'Yes' button
        private void button1_Click(object sender, EventArgs e)
        {
            String filename = "files.txt";
            List<FileInfo> fl = (List<FileInfo>)form1.selected;

            foreach (FileInfo fi in fl)
            {   
                if(Directory.GetDirectoryRoot(fi.DirectoryName) == form1.getDriveLetter)
                {
                    String caption = "Error";
                    String message = "Error! File(s) selected is already in " + form1.getDriveLetter;
                    DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.OK);
                    break;
                }
                //Writes files to be added to a text file that can be read from later.
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename), true))
                {
                    sw.WriteLine(fi.ToString());
                    //Check if file is already present in flash drive, if not then adds it to flash drive
                    if (form1.searchFlashDrive(fi) == false)
                    {
                        File.Copy(Path.Combine(fi.Directory.FullName, fi.ToString()), Path.Combine(form1.getDriveLetter, fi.ToString()), true);
                    }
                }
            }
            // Updates listbox in Main
            form1.getList.Update();
            this.Close();
        }
    }
}

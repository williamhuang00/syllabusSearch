using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;

namespace RubricSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDirectory_Click(object sender, EventArgs e) //user clicks add directory button to trigger this function
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog(); //menu pops up to allow user to choose a directory
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                lstbFolder.Items.Clear();
                string[] files = Directory.GetFiles(fbd.SelectedPath); //get every file in the chosen directory

                foreach (string file in files)
                {
                    if (file.EndsWith(".txt")) //we are only interested in txt files for now, maybe modify this for pdfs/word docs?
                    {
                        lstbFolder.Items.Add(file);
                    }
                }
            }
        }

        private void lstbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keyWord = txtKeyWord.Text; //currently this program only checks for 1 key word across each document
            string filePath = lstbFolder.Items[lstbFolder.SelectedIndex].ToString();
            ArrayList lines = new ArrayList();
            int keyWordCount = 0;
            StreamReader textIn = new StreamReader(new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read));
            while (textIn.Peek() != -1) //keep searching until there's nothing in the file left to read
            {
                string row = textIn.ReadLine(); //we'll read line by line
                if (row.Contains(keyWord)) //if the current line contains out keyword update some statistics like keyWordCount and save that line
                {
                    lines.Add(row.ToString() + Environment.NewLine);
                    keyWordCount++;
                }


            }
            string linesOutput = "";
            foreach (string item in lines) // ArrayList tostring doesn't give us meaningful data, so we're looping through the arraylist manually to grab the lines
            {
                linesOutput += Environment.NewLine + item.ToString();
            }
            txtOutput.Text = keyWord + " occurred in " + keyWordCount.ToString() + " part(s) of the document" + Environment.NewLine +
                linesOutput;
        }

        private void txtKeyWord_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

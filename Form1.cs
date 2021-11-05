
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RubricSearch
{
  public class Form1 : Form
  {
    private List<KnowledgeUnit> allUnits = new List<KnowledgeUnit>();
    private Dictionary<string, string> courseSyllabi = new Dictionary<string, string>();
    private List<string> results = new List<string>();
    private IContainer components = (IContainer) null;
    private Label lblKeyWord;
    private TextBox txtKeyWord;
    private Button btnExit;
    private ListBox lstbSyllabi;
    private Button txtDirectory;
    private Button txtFontBigger;
    private Button txtFontSmaller;
    private ListBox lstbKnowledgeUnits;
    private Button btnKnowledgeUnits;
    private TextBox txtOutput;
    private Button btnGenerate;

    public Form1() => this.InitializeComponent();

    private void btnExit_Click(object sender, EventArgs e) => this.Close();

    private void txtDirectory_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
      this.courseSyllabi.Clear();
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      this.lstbSyllabi.Items.Clear();
      foreach (string file in Directory.GetFiles(folderBrowserDialog.SelectedPath))
      {
        if (file.EndsWith(".txt"))
        {
          string[] strArray = file.Split('\\');
          string key = strArray[strArray.Length - 1];
          this.courseSyllabi.Add(key, file);
          this.lstbSyllabi.Items.Add((object) key);
        }
      }
    }

    private void lstbFolder_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lstbSyllabi.Items.Count == 0 || this.lstbSyllabi.SelectedIndex == -1)
        return;
      string courseSyllabus = this.courseSyllabi[this.lstbSyllabi.Items[this.lstbSyllabi.SelectedIndex].ToString()];
      List<string> topics = this.allUnits[this.lstbKnowledgeUnits.SelectedIndex].topics;
      StreamReader streamReader = new StreamReader((Stream) new FileStream(courseSyllabus, FileMode.OpenOrCreate, FileAccess.Read));
      string str1 = "";
      int num = 0;
      while (streamReader.Peek() != -1)
      {
        string lower = streamReader.ReadLine().Trim().ToLower();
        str1 = str1 + lower + " ";
      }
      string str2 = "";
      foreach (string str3 in topics)
      {
        if (!str3.Equals("") && str1.Contains(str3.Trim().ToLower()))
        {
          ++num;
          str2 = str2 + str3 + Environment.NewLine;
        }
      }
      this.txtOutput.Text = num.ToString() + " Match(es)" + Environment.NewLine + str2;
    }

    private void txtKeyWord_TextChanged(object sender, EventArgs e) => this.lstbFolder_SelectedIndexChanged((object) null, (EventArgs) null);

    private void txtFontSmaller_Click(object sender, EventArgs e) => this.txtOutput.Font = new Font(this.txtOutput.Font.Name, this.txtOutput.Font.Size - 1f, this.txtOutput.Font.Style, this.txtOutput.Font.Unit);

    private void txtFontBigger_Click(object sender, EventArgs e) => this.txtOutput.Font = new Font(this.txtOutput.Font.Name, this.txtOutput.Font.Size + 1f, this.txtOutput.Font.Style, this.txtOutput.Font.Unit);

    private void cbAlwaysOnTop_CheckedChanged(object sender, EventArgs e) => this.TopMost = !this.TopMost;

    private void btnKnowledgeUnits_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      this.results.Clear();
      this.allUnits.Clear();
      this.lstbKnowledgeUnits.Items.Clear();
      foreach (string file in Directory.GetFiles(folderBrowserDialog.SelectedPath))
      {
        KnowledgeUnit knowledgeUnit = new KnowledgeUnit(file.ToString());
        this.allUnits.Add(knowledgeUnit);
        if (file.EndsWith(".txt"))
          this.lstbKnowledgeUnits.Items.Add((object) knowledgeUnit.getAcronym());
      }
    }

    private void lstbKnowledgeUnits_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lstbKnowledgeUnits.SelectedIndex == -1)
        return;
      this.txtKeyWord.Text = this.allUnits[this.lstbKnowledgeUnits.SelectedIndex].ToString();
    }

    private void btnClipBoard_Click(object sender, EventArgs e) => Clipboard.SetText(this.txtOutput.Text);

    public void addRow(string row, string filePath)
    {
      try
      {
        using (StreamWriter streamWriter = new StreamWriter(filePath, true))
          streamWriter.WriteLine(row);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.StackTrace);
      }
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
      this.txtOutput.Text = "Report in progress. Please wait. ";
      int num1 = 0;
      for (int count = this.lstbKnowledgeUnits.Items.Count; num1 < count; ++num1)
      {
        this.lstbKnowledgeUnits.SelectedIndex = num1;
        this.results.Clear();
        for (int index = 0; index < this.lstbSyllabi.Items.Count; ++index)
          this.results.Add(this.allUnits[this.lstbKnowledgeUnits.SelectedIndex].checkForRequirements(this.courseSyllabi[this.lstbSyllabi.Items[index].ToString()]));
        this.txtOutput.Text = "";
        for (int index = 0; index < this.results.Count; ++index)
          this.txtOutput.Text += this.results[index].ToString();
        string str1 = "";
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Directory.CreateDirectory(folderPath + "\\KuResults");
        for (int index = 0; index < this.allUnits[this.lstbKnowledgeUnits.SelectedIndex].topics.Count; ++index)
        {
          string str2 = "" + "\"" + this.allUnits[this.lstbKnowledgeUnits.SelectedIndex].topics[index] + "\",";
          foreach (string result in this.results)
          {
            char[] chArray = new char[1]{ '\n' };
            string[] strArray = result.Split(chArray);
            if (index < strArray.Length)
              str2 = str2 + strArray[index] + ",";
          }
          str1 = str1 + str2.Replace("\n", "").Replace("\r", "") + Environment.NewLine;
          string str3 = folderPath + "\\KuResults\\" + this.allUnits[this.lstbKnowledgeUnits.SelectedIndex].getAcronym() + ".csv";
          if (index == 0 && File.Exists(str3))
          {
            try
            {
              File.Delete(str3);
            }
            catch (IOException ex)
            {
              int num2 = (int) MessageBox.Show("Something went wrong. Don't panic." + Environment.NewLine + "You have opened a file from the KuResults folder. Please close it and re-run the report.");
              this.txtOutput.Text = "Please re-run the report.";
              return;
            }
          }
          this.addRow(str2.Replace("\n", "").Replace("\r", ""), str3);
        }
        string str4 = "";
        foreach (string result in this.results)
        {
          char[] chArray = new char[1]{ '\n' };
          string[] strArray = result.Split(chArray);
          str4 = str4 + strArray[strArray.Length - 1] + ",";
        }
        string str5 = "Percentages," + str4;
        string filePath = folderPath + "\\KuResults\\" + this.allUnits[this.lstbKnowledgeUnits.SelectedIndex].getAcronym() + ".csv";
        this.addRow(str5.Replace("\n", "").Replace("\r", ""), filePath);
      }
      this.txtOutput.Text = "Report finished. Check your desktop for KuResults Folder";
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblKeyWord = new Label();
      this.txtKeyWord = new TextBox();
      this.btnExit = new Button();
      this.lstbSyllabi = new ListBox();
      this.txtDirectory = new Button();
      this.txtFontBigger = new Button();
      this.txtFontSmaller = new Button();
      this.lstbKnowledgeUnits = new ListBox();
      this.btnKnowledgeUnits = new Button();
      this.txtOutput = new TextBox();
      this.btnGenerate = new Button();
      this.SuspendLayout();
      this.lblKeyWord.Location = new Point(12, 22);
      this.lblKeyWord.Name = "lblKeyWord";
      this.lblKeyWord.Size = new Size(149, 57);
      this.lblKeyWord.TabIndex = 0;
      this.lblKeyWord.Text = "Terms being searched";
      this.txtKeyWord.Location = new Point(167, 19);
      this.txtKeyWord.Multiline = true;
      this.txtKeyWord.Name = "txtKeyWord";
      this.txtKeyWord.ReadOnly = true;
      this.txtKeyWord.ScrollBars = ScrollBars.Both;
      this.txtKeyWord.Size = new Size(379, 129);
      this.txtKeyWord.TabIndex = 1;
      this.txtKeyWord.TextChanged += new EventHandler(this.txtKeyWord_TextChanged);
      this.btnExit.Location = new Point(501, 534);
      this.btnExit.Name = "btnExit";
      this.btnExit.Size = new Size(75, 23);
      this.btnExit.TabIndex = 3;
      this.btnExit.Text = "Exit";
      this.btnExit.UseVisualStyleBackColor = true;
      this.btnExit.Click += new EventHandler(this.btnExit_Click);
      this.lstbSyllabi.FormattingEnabled = true;
      this.lstbSyllabi.Location = new Point(300, 183);
      this.lstbSyllabi.Name = "lstbSyllabi";
      this.lstbSyllabi.Size = new Size(269, 69);
      this.lstbSyllabi.TabIndex = 5;
      this.lstbSyllabi.SelectedIndexChanged += new EventHandler(this.lstbFolder_SelectedIndexChanged);
      this.txtDirectory.Location = new Point(332, 154);
      this.txtDirectory.Name = "txtDirectory";
      this.txtDirectory.Size = new Size(214, 23);
      this.txtDirectory.TabIndex = 6;
      this.txtDirectory.Text = "Location of Syllabus";
      this.txtDirectory.UseVisualStyleBackColor = true;
      this.txtDirectory.Click += new EventHandler(this.txtDirectory_Click);
      this.txtFontBigger.Location = new Point(93, 533);
      this.txtFontBigger.Name = "txtFontBigger";
      this.txtFontBigger.Size = new Size(75, 23);
      this.txtFontBigger.TabIndex = 7;
      this.txtFontBigger.Text = "Bigger Font";
      this.txtFontBigger.UseVisualStyleBackColor = true;
      this.txtFontBigger.Click += new EventHandler(this.txtFontBigger_Click);
      this.txtFontSmaller.Location = new Point(12, 533);
      this.txtFontSmaller.Name = "txtFontSmaller";
      this.txtFontSmaller.Size = new Size(75, 23);
      this.txtFontSmaller.TabIndex = 8;
      this.txtFontSmaller.Text = "Smaller Font";
      this.txtFontSmaller.UseVisualStyleBackColor = true;
      this.txtFontSmaller.Click += new EventHandler(this.txtFontSmaller_Click);
      this.lstbKnowledgeUnits.FormattingEnabled = true;
      this.lstbKnowledgeUnits.Location = new Point(15, 183);
      this.lstbKnowledgeUnits.Name = "lstbKnowledgeUnits";
      this.lstbKnowledgeUnits.Size = new Size(269, 69);
      this.lstbKnowledgeUnits.TabIndex = 10;
      this.lstbKnowledgeUnits.SelectedIndexChanged += new EventHandler(this.lstbKnowledgeUnits_SelectedIndexChanged);
      this.btnKnowledgeUnits.Location = new Point(38, 154);
      this.btnKnowledgeUnits.Name = "btnKnowledgeUnits";
      this.btnKnowledgeUnits.Size = new Size(214, 23);
      this.btnKnowledgeUnits.TabIndex = 11;
      this.btnKnowledgeUnits.Text = "Location of Knowledge Units";
      this.btnKnowledgeUnits.UseVisualStyleBackColor = true;
      this.btnKnowledgeUnits.Click += new EventHandler(this.btnKnowledgeUnits_Click);
      this.txtOutput.Location = new Point(15, 258);
      this.txtOutput.Multiline = true;
      this.txtOutput.Name = "txtOutput";
      this.txtOutput.ReadOnly = true;
      this.txtOutput.ScrollBars = ScrollBars.Both;
      this.txtOutput.Size = new Size(561, 238);
      this.txtOutput.TabIndex = 4;
      this.btnGenerate.Location = new Point(174, 534);
      this.btnGenerate.Name = "btnGenerate";
      this.btnGenerate.Size = new Size(115, 23);
      this.btnGenerate.TabIndex = 13;
      this.btnGenerate.Text = "Generate report";
      this.btnGenerate.UseVisualStyleBackColor = true;
      this.btnGenerate.Click += new EventHandler(this.btnGenerate_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(587, 568);
      this.Controls.Add((Control) this.btnGenerate);
      this.Controls.Add((Control) this.btnKnowledgeUnits);
      this.Controls.Add((Control) this.lstbKnowledgeUnits);
      this.Controls.Add((Control) this.txtFontSmaller);
      this.Controls.Add((Control) this.txtFontBigger);
      this.Controls.Add((Control) this.txtDirectory);
      this.Controls.Add((Control) this.lstbSyllabi);
      this.Controls.Add((Control) this.txtOutput);
      this.Controls.Add((Control) this.btnExit);
      this.Controls.Add((Control) this.txtKeyWord);
      this.Controls.Add((Control) this.lblKeyWord);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Name = nameof (Form1);
      this.Text = "SyllabusSearch";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}

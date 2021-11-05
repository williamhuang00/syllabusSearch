
using System;
using System.Collections.Generic;
using System.IO;

namespace RubricSearch
{
  internal class KnowledgeUnit
  {
    public List<string> topics = new List<string>();

    public string Acronym { get; set; }

    public KnowledgeUnit(string filePath)
    {
      StreamReader streamReader = new StreamReader((Stream) new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read));
      this.Acronym = streamReader.ReadLine();
      while (streamReader.Peek() != -1)
      {
        string input = streamReader.ReadLine();
        if (input.EndsWith(","))
          input = input + " " + streamReader.ReadLine().Trim();
        if (input.StartsWith("       ") && input.EndsWith("."))
          this.topics[this.topics.Count - 1] = this.topics[this.topics.Count - 1] + " " + input.ToLower().Trim();
        else if (input.StartsWith("                "))
        {
          if (input.Contains("etc."))
            this.topics[this.topics.Count - 1] = this.topics[this.topics.Count - 1] + " " + input.ToLower().Trim();
          else
            this.topics.Add(this.removePrefix(input).ToLower().Trim());
        }
        else if (input.StartsWith("         "))
          this.topics.Add(this.removePrefix(input).ToLower().Trim());
        else if (input.StartsWith("          "))
          this.topics.Add(this.removePrefix(input).ToLower().Trim());
        else if (input.StartsWith("           "))
          this.topics.Add(this.removePrefix(input).ToLower().Trim());
        else if (input.StartsWith("       ") && !input.Replace("       ", "").Contains("."))
          this.topics[this.topics.Count - 1] = this.topics[this.topics.Count - 1] + " " + input.Replace("       ", "");
        else
          this.topics.Add(this.removePrefix(input).Trim());
      }
    }

    public string checkForRequirements(string path)
    {
      string str1 = "";
      string[] strArray = path.Split('\\');
      StreamReader streamReader = new StreamReader((Stream) new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read));
      string str2 = "";
      while (streamReader.Peek() != -1)
      {
        string str3 = streamReader.ReadLine();
        if (str3.EndsWith(","))
          str3 = str3 + " " + streamReader.ReadLine().ToLower().Trim();
        string str4 = !str3.StartsWith("       ") ? str3.Trim().ToLower() : str3.Replace("       ", " ").ToLower();
        str2 += str4;
      }
      string str5 = "";
      double num1 = 0.0;
      double num2 = 0.0;
      for (int index = 1; index < this.topics.Count; ++index)
      {
        if (this.topics[index].ToString() == ")")
          ++num2;
        else if (this.topics[index].ToLower().Trim() != "" && str2.Contains(this.topics[index].ToString().Trim().ToLower()))
        {
          str1 = str1 + "Yes" + Environment.NewLine;
          str5 = str5 + this.topics[index].ToLower().Trim() + " ---> SATISFIED" + Environment.NewLine;
          ++num1;
        }
        else if (string.IsNullOrEmpty(this.topics[index].ToLower().Trim()) || string.IsNullOrWhiteSpace(this.topics[index].ToLower().Trim()))
        {
          ++num2;
        }
        else
        {
          str1 = str1 + "No" + Environment.NewLine;
          str5 = str5 + this.topics[index].ToLower().Trim() + " --> NOT SATISFIED" + Environment.NewLine;
        }
      }
      double num3 = (double) (this.topics.Count - 1) - num2;
      return strArray[strArray.Length - 1].Replace(".txt", "") + Environment.NewLine + str1 + (100.0 * (num1 / num3)).ToString("0.00") + "%";
    }

    public List<string> getTopics() => this.topics;

    public string removePrefix(string input)
    {
      for (int index = 0; index < input.Length; ++index)
      {
        if (input[index] == '.')
          return input.Substring(index + 1);
      }
      return "";
    }

    public string getAcronym() => this.Acronym;

    public override string ToString()
    {
      string str = "";
      for (int index = 0; index < this.topics.Count; ++index)
        str = str + this.topics[index] + Environment.NewLine;
      return str;
    }
  }
}

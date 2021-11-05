// Decompiled with JetBrains decompiler
// Type: RubricSearch.Properties.Settings
// Assembly: RubricSearch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A34E6137-69CB-4B7F-B2F8-C9F6F830413B
// Assembly location: C:\Users\Will\Downloads\Parsing Tool\Parsing Tool\RubricSearch.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace RubricSearch.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }
  }
}

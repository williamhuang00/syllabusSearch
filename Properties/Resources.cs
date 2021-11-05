// Decompiled with JetBrains decompiler
// Type: RubricSearch.Properties.Resources
// Assembly: RubricSearch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A34E6137-69CB-4B7F-B2F8-C9F6F830413B
// Assembly location: C:\Users\Will\Downloads\Parsing Tool\Parsing Tool\RubricSearch.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace RubricSearch.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (RubricSearch.Properties.Resources.resourceMan == null)
          RubricSearch.Properties.Resources.resourceMan = new ResourceManager("RubricSearch.Properties.Resources", typeof (RubricSearch.Properties.Resources).Assembly);
        return RubricSearch.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => RubricSearch.Properties.Resources.resourceCulture;
      set => RubricSearch.Properties.Resources.resourceCulture = value;
    }
  }
}

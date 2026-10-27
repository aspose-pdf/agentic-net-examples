using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class ViewerPreferencesMapper
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "customized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Attempt to retrieve ViewerPreferences via dynamic to stay compatible with older Aspose.Pdf versions
            dynamic prefs = null;
            try
            {
                prefs = ((dynamic)doc).ViewerPreferences;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. The PDF will be saved unchanged.");
            }

            if (prefs != null)
            {
                // Helper to read/write boolean properties via reflection (works for both old and new versions)
                void SetBool(string propName, bool currentValue)
                {
                    bool newValue = PromptToggle(propName, currentValue);
                    var prop = prefs.GetType().GetProperty(propName);
                    if (prop != null && prop.CanWrite)
                        prop.SetValue(prefs, newValue);
                }

                // Map known boolean viewer preference properties
                SetBool(nameof(prefs.HideToolbar), prefs.HideToolbar);
                SetBool(nameof(prefs.HideMenubar), prefs.HideMenubar);
                SetBool(nameof(prefs.HideWindowUI), prefs.HideWindowUI);
                SetBool(nameof(prefs.FitWindow), prefs.FitWindow);
                SetBool(nameof(prefs.CenterWindow), prefs.CenterWindow);
                SetBool(nameof(prefs.DisplayDocTitle), prefs.DisplayDocTitle);
                SetBool(nameof(prefs.NonFullScreenPageMode), prefs.NonFullScreenPageMode);

                // Direction handling – if the enum exists, toggle between RightToLeft and LeftToRight
                var directionProp = prefs.GetType().GetProperty("Direction");
                if (directionProp != null && directionProp.CanWrite && directionProp.CanRead)
                {
                    var currentDir = directionProp.GetValue(prefs);
                    // Try to resolve enum names via reflection; fallback to string comparison
                    string rightToLeftName = "RightToLeft";
                    string leftToRightName = "LeftToRight";
                    bool isRtl = currentDir != null && currentDir.ToString().EndsWith(rightToLeftName);
                    bool newIsRtl = PromptToggle("Right-to-Left Reading Direction", isRtl);
                    if (newIsRtl)
                    {
                        // Set to RightToLeft enum value if possible
                        var enumType = directionProp.PropertyType;
                        var rtlValue = Enum.Parse(enumType, rightToLeftName);
                        directionProp.SetValue(prefs, rtlValue);
                    }
                    else
                    {
                        var enumType = directionProp.PropertyType;
                        var ltrValue = Enum.Parse(enumType, leftToRightName);
                        directionProp.SetValue(prefs, ltrValue);
                    }
                }
            }

            // Save the (potentially) modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Customized PDF saved to '{outputPath}'.");
    }

    // Helper method to prompt the user for a Y/N toggle and return the resulting boolean
    static bool PromptToggle(string description, bool currentValue)
    {
        while (true)
        {
            Console.Write($"{description} (current: {(currentValue ? "Y" : "N")}): ");
            string input = Console.ReadLine()?.Trim().ToUpperInvariant();

            if (string.IsNullOrEmpty(input))
                return currentValue; // keep current if no input

            if (input == "Y")
                return true;
            if (input == "N")
                return false;

            Console.WriteLine("Please enter 'Y' or 'N'.");
        }
    }
}

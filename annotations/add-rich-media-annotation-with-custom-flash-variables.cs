using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that page 3 exists
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            Page page = doc.Pages[3];

            // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation
            var richMedia = new RichMediaAnnotation(page, rect);

            // Try to set custom Flash variables using reflection – works with both newer and older Aspose.Pdf versions
            // Newer versions expose RichMediaSettings.CustomFlashVariables
            Type richMediaSettingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            if (richMediaSettingsType != null)
            {
                // Create an instance of RichMediaSettings
                object settingsInstance = Activator.CreateInstance(richMediaSettingsType);

                // Prepare the dictionary of variables
                var flashVars = new Dictionary<string, string>
                {
                    { "var1", "value1" },
                    { "var2", "value2" }
                };

                // Set the CustomFlashVariables property on the settings instance
                var customFlashProp = richMediaSettingsType.GetProperty("CustomFlashVariables");
                customFlashProp?.SetValue(settingsInstance, flashVars);

                // Assign the settings object to the annotation (property may be named RichMediaSettings)
                var richMediaSettingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                richMediaSettingsProp?.SetValue(richMedia, settingsInstance);
            }
            else
            {
                // Older versions may expose CustomFlashVariables directly on the annotation
                var directFlashProp = typeof(RichMediaAnnotation).GetProperty("CustomFlashVariables");
                if (directFlashProp != null)
                {
                    var flashVars = new Dictionary<string, string>
                    {
                        { "var1", "value1" },
                        { "var2", "value2" }
                    };
                    directFlashProp.SetValue(richMedia, flashVars);
                }
                else
                {
                    // If neither property exists, we simply add the annotation without custom variables.
                    Console.Error.WriteLine("CustomFlashVariables property not available in this Aspose.Pdf version; annotation added without them.");
                }
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Apply the changes
            richMedia.Update();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMedia annotation added and saved to '{outputPath}'.");
    }
}

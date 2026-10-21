using System;
using System.IO;
using System.Text.Json;
using System.Reflection;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class ViewerPreferencesConfig
{
    public bool HideToolbar { get; set; }
    public bool HideMenubar { get; set; }
    public bool CenterWindow { get; set; }
    public bool DisplayDocTitle { get; set; }
    public bool HideWindowUI { get; set; }
    public bool FitWindow { get; set; }
    public bool PrintScaling { get; set; }
    // Add other viewer preference properties as needed, matching the JSON structure.
}

class Program
{
    static void Main()
    {
        const string configPath = "viewerPreferences.json";   // JSON file with the settings
        const string inputFolder = "InputPdfs";              // Folder containing PDFs to process
        const string outputFolder = "OutputPdfs";            // Folder to write modified PDFs

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Deserialize JSON configuration into a strongly‑typed object
        ViewerPreferencesConfig? config = JsonSerializer.Deserialize<ViewerPreferencesConfig>(File.ReadAllText(configPath));
        if (config == null)
        {
            Console.Error.WriteLine("Failed to parse configuration.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // -----------------------------------------------------------------
                // Apply viewer preferences – use reflection so the code compiles with
                // older Aspose.Pdf versions that do not expose the ViewerPreferences
                // property. If the property (or the individual preference members) is
                // missing, we simply skip the setting and write a diagnostic message.
                // -----------------------------------------------------------------
                try
                {
                    PropertyInfo? viewerPrefProp = doc.GetType().GetProperty("ViewerPreferences");
                    if (viewerPrefProp != null)
                    {
                        object? prefs = viewerPrefProp.GetValue(doc);
                        if (prefs != null)
                        {
                            SetPreference(prefs, "HideToolbar", config.HideToolbar);
                            SetPreference(prefs, "HideMenubar", config.HideMenubar);
                            SetPreference(prefs, "CenterWindow", config.CenterWindow);
                            SetPreference(prefs, "DisplayDocTitle", config.DisplayDocTitle);
                            SetPreference(prefs, "HideWindowUI", config.HideWindowUI);
                            SetPreference(prefs, "FitWindow", config.FitWindow);
                            SetPreference(prefs, "PrintScaling", config.PrintScaling);
                            // Add additional preferences here as needed.
                        }
                    }
                    else
                    {
                        Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf library version.");
                    }
                }
                catch (RuntimeBinderException ex)
                {
                    // This catch is a safety net for dynamic access scenarios; it should not be hit
                    // when using reflection, but we keep it for completeness.
                    Console.WriteLine($"Runtime binder error while setting viewer preferences: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error while applying viewer preferences: {ex.Message}");
                }

                // Save the modified PDF (overwrites or creates a new file)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }

        Console.WriteLine("All PDFs have been updated with viewer preferences where supported.");
    }

    /// <summary>
    /// Sets a boolean viewer‑preference property via reflection if the property exists.
    /// </summary>
    private static void SetPreference(object prefsObject, string propertyName, bool value)
    {
        PropertyInfo? prop = prefsObject.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (prop != null && prop.CanWrite)
        {
            prop.SetValue(prefsObject, value);
        }
        else
        {
            // Property not found – most likely the running Aspose.Pdf version does not support it.
            // We silently ignore it to keep the program robust.
        }
    }
}

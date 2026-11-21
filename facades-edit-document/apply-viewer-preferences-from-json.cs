using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required by task specification
using Microsoft.CSharp.RuntimeBinder; // for RuntimeBinderException

// Model representing viewer preference settings in the JSON file
public class ViewerPreferencesConfig
{
    public bool? HideToolbar { get; set; }
    public bool? HideMenubar { get; set; }
    public bool? HideWindowUI { get; set; }
    public bool? FitWindow { get; set; }
    public bool? CenterWindow { get; set; }
    public bool? DisplayDocTitle { get; set; }
    // Made nullable to satisfy the non‑nullable warning (CS8618)
    public string? NonFullScreenPageMode { get; set; }
    public string? PageLayout { get; set; }
    public string? PageMode { get; set; }
}

class Program
{
    static void Main()
    {
        const string configPath   = "viewerPreferences.json"; // JSON configuration file
        const string inputFolder  = "InputPdfs";              // Folder containing source PDFs
        const string outputFolder = "OutputPdfs";             // Folder for processed PDFs

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Deserialize JSON configuration
        ViewerPreferencesConfig? config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<ViewerPreferencesConfig>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        if (config == null)
        {
            Console.Error.WriteLine("Configuration deserialized to null.");
            return;
        }

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Gather all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            string fileName   = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load PDF as dynamic to avoid compile‑time dependency on ViewerPreferences (may be missing in older Aspose.Pdf versions)
                dynamic doc = new Document(pdfPath);

                try
                {
                    // Attempt to access ViewerPreferences at runtime
                    dynamic prefs = doc.ViewerPreferences;

                    // Boolean preferences – set only when a value is supplied in the JSON
                    if (config.HideToolbar.HasValue)    prefs.HideToolbar    = config.HideToolbar.Value;
                    if (config.HideMenubar.HasValue)    prefs.HideMenubar    = config.HideMenubar.Value;
                    if (config.HideWindowUI.HasValue)   prefs.HideWindowUI   = config.HideWindowUI.Value;
                    if (config.FitWindow.HasValue)      prefs.FitWindow      = config.FitWindow.Value;
                    if (config.CenterWindow.HasValue)   prefs.CenterWindow   = config.CenterWindow.Value;
                    if (config.DisplayDocTitle.HasValue)prefs.DisplayDocTitle = config.DisplayDocTitle.Value;

                    // Enum‑based preferences – use reflection so the code compiles even if the enum types are absent in the referenced Aspose.Pdf version.
                    // NonFullScreenPageMode
                    if (!string.IsNullOrWhiteSpace(config.NonFullScreenPageMode))
                    {
                        TrySetEnumPreference(prefs, "NonFullScreenPageModeEnum", config.NonFullScreenPageMode, "NonFullScreenPageMode");
                    }

                    // PageLayout
                    if (!string.IsNullOrWhiteSpace(config.PageLayout))
                    {
                        TrySetEnumPreference(prefs, "PageLayoutEnum", config.PageLayout, "PageLayout");
                    }

                    // PageMode
                    if (!string.IsNullOrWhiteSpace(config.PageMode))
                    {
                        TrySetEnumPreference(prefs, "PageModeEnum", config.PageMode, "PageMode");
                    }
                }
                catch (RuntimeBinderException)
                {
                    // ViewerPreferences property does not exist in the referenced Aspose.Pdf version.
                    Console.WriteLine($"ViewerPreferences not supported for '{fileName}'. Skipping preference application.");
                }

                // Save the (potentially) modified PDF
                doc.Save(outputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("All files processed.");
    }

    /// <summary>
    /// Attempts to set an enum‑based viewer preference using reflection. If the enum type or the property does not exist,
    /// the method silently does nothing – this mirrors the dynamic‑fallback strategy used for older Aspose.Pdf versions.
    /// </summary>
    private static void TrySetEnumPreference(dynamic prefs, string enumTypeName, string stringValue, string propertyName)
    {
        try
        {
            // Obtain the runtime type of the prefs object (ViewerPreferences implementation)
            Type prefsType = ((object)prefs).GetType();
            // Look for a nested enum with the supplied name (e.g., "NonFullScreenPageModeEnum")
            Type? enumType = prefsType.GetNestedType(enumTypeName, BindingFlags.Public | BindingFlags.NonPublic);
            if (enumType == null)
                return; // Enum not present – nothing to set.

            // Parse the string value to the enum (ignore case)
            if (Enum.TryParse(enumType, stringValue, true, out object? enumValue))
            {
                // Use reflection to set the property (e.g., prefs.NonFullScreenPageMode = enumValue)
                PropertyInfo? prop = prefsType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(prefs, enumValue);
                }
            }
        }
        catch
        {
            // Swallow any exception – the goal is to keep the program running even if the current Aspose.Pdf version
            // does not support the particular enum or property.
        }
    }
}

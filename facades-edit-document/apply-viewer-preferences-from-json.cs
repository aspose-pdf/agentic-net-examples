using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ViewerPreferenceConfig
{
    // List of PDF file paths to process
    public List<string> PdfFiles { get; set; }

    // List of viewer preference names (e.g., "HideMenubar", "PageModeUseNone")
    public List<string> Preferences { get; set; }
}

class Program
{
    static void Main()
    {
        const string configPath = "viewerPreferences.json";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Deserialize JSON configuration
        ViewerPreferenceConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<ViewerPreferenceConfig>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        if (config?.PdfFiles == null || config.PdfFiles.Count == 0)
        {
            Console.Error.WriteLine("No PDF files specified in configuration.");
            return;
        }

        // Compute combined preference flag from the list of names
        int combinedPreference = 0;
        foreach (string prefName in config.Preferences ?? new List<string>())
        {
            // ViewerPreference fields are defined as const ints
            FieldInfo field = typeof(ViewerPreference).GetField(prefName, BindingFlags.Public | BindingFlags.Static);
            if (field != null && field.FieldType == typeof(int))
            {
                combinedPreference |= (int)field.GetValue(null);
            }
            else
            {
                Console.Error.WriteLine($"Warning: ViewerPreference '{prefName}' not found.");
            }
        }

        // Process each PDF file
        foreach (string pdfPath in config.PdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF file not found: {pdfPath}");
                continue;
            }

            // Initialize the facade
            PdfContentEditor editor = new PdfContentEditor();
            try
            {
                editor.BindPdf(pdfPath);

                // Apply the combined viewer preference
                editor.ChangeViewerPreference(combinedPreference);

                // Save to a new file (original name with suffix)
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(pdfPath),
                    Path.GetFileNameWithoutExtension(pdfPath) + "_pref.pdf");

                editor.Save(outputPath);
                Console.WriteLine($"Processed '{pdfPath}' -> '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
            finally
            {
                // Dispose the facade if it implements IDisposable
                if (editor is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}
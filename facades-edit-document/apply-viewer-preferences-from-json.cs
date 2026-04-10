using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;          // PdfContentEditor, ViewerPreference
using Aspose.Pdf;                  // ViewerPreference resides here

// Model representing the JSON configuration file.
public class ViewerPreferenceConfig
{
    // List of viewer preference names (e.g., "HideMenubar", "FitWindow").
    public List<string>? ViewerPreferences { get; set; }
}

class Program
{
    static void Main()
    {
        // Paths – adjust as needed.
        const string configPath   = "viewerPreferences.json";   // JSON config file
        const string inputFolder  = "InputPdfs";               // Folder with source PDFs
        const string outputFolder = "OutputPdfs";              // Folder for processed PDFs

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load and deserialize the JSON configuration.
        ViewerPreferenceConfig? config;
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

        if (config?.ViewerPreferences == null || config.ViewerPreferences.Count == 0)
        {
            Console.Error.WriteLine("No viewer preferences defined in the configuration.");
            return;
        }

        // Translate preference names to their integer flag values.
        int combinedPreference = 0;
        foreach (string prefName in config.ViewerPreferences)
        {
            var field = typeof(ViewerPreference).GetField(prefName);
            if (field == null)
            {
                Console.Error.WriteLine($"Unknown ViewerPreference: {prefName}");
                continue;
            }

            if (field.GetValue(null) is int value)
                combinedPreference |= value;
        }

        if (combinedPreference == 0)
        {
            Console.Error.WriteLine("No valid viewer preferences were parsed.");
            return;
        }

        // Ensure output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder.
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Bind the PDF, apply the combined viewer preferences, and save.
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(pdfPath);
                editor.ChangeViewerPreference(combinedPreference);
                editor.Save(outputPath);
                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("All files processed.");
    }
}
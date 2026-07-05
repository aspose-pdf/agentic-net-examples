using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    // Model for JSON configuration
    private class ViewerConfig
    {
        public string[] Preferences { get; set; }
    }

    static void Main()
    {
        const string configPath = "viewerConfig.json";   // JSON file with viewer preferences
        const string inputFolder = "InputPdfs";          // Folder containing PDFs to process
        const string outputFolder = "OutputPdfs";        // Folder to save modified PDFs

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load and deserialize JSON configuration
        ViewerConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<ViewerConfig>(json);
            if (config?.Preferences == null || config.Preferences.Length == 0)
            {
                Console.Error.WriteLine("No viewer preferences defined in the configuration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Resolve ViewerPreference flags from string names
        int combinedFlags = 0;
        foreach (string prefName in config.Preferences)
        {
            var field = typeof(ViewerPreference).GetField(prefName);
            if (field == null)
            {
                Console.Error.WriteLine($"Unknown ViewerPreference: {prefName}");
                continue;
            }
            combinedFlags |= (int)field.GetValue(null);
        }

        if (combinedFlags == 0)
        {
            Console.Error.WriteLine("No valid viewer preferences were resolved.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Bind the PDF to the facade
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(pdfPath);

                // Apply the combined viewer preferences
                editor.ChangeViewerPreference(combinedFlags);

                // Save the modified PDF
                editor.Save(outputPath);

                Console.WriteLine($"Processed '{fileName}' -> '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}
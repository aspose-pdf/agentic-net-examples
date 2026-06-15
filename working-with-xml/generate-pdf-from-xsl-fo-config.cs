using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    // Configuration model matching the JSON structure.
    private class Config
    {
        public string XslFoFilePath { get; set; }
        public string OutputPdfPath { get; set; }
    }

    static void Main()
    {
        const string configPath = "config.json";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration from JSON.
        Config cfg;
        try
        {
            string json = File.ReadAllText(configPath);
            cfg = JsonSerializer.Deserialize<Config>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        if (string.IsNullOrWhiteSpace(cfg?.XslFoFilePath) || string.IsNullOrWhiteSpace(cfg?.OutputPdfPath))
        {
            Console.Error.WriteLine("Configuration must contain XslFoFilePath and OutputPdfPath.");
            return;
        }

        if (!File.Exists(cfg.XslFoFilePath))
        {
            Console.Error.WriteLine($"XSL-FO file not found: {cfg.XslFoFilePath}");
            return;
        }

        // Initialize load options with the XSL-FO file.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions(cfg.XslFoFilePath);

        // Load the XSL-FO document and convert it to PDF.
        using (Document pdfDocument = new Document(cfg.XslFoFilePath, loadOptions))
        {
            // Save the resulting PDF to the path specified in the configuration.
            pdfDocument.Save(cfg.OutputPdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{cfg.OutputPdfPath}'.");
    }
}
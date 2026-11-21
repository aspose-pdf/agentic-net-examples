using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    // Classes that map to the JSON structure in appsettings.json
    private class Settings
    {
        public PathsConfig Paths { get; set; }
    }

    private class PathsConfig
    {
        public string Xml { get; set; }
        public string Xslt { get; set; }
        public string OutputPdf { get; set; }
    }

    static void Main()
    {
        // ------------------------------------------------------------
        // Load configuration from appsettings.json using System.Text.Json
        // ------------------------------------------------------------
        string configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        Settings config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Settings>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        if (config?.Paths == null)
        {
            Console.Error.WriteLine("Invalid configuration format – missing 'Paths' section.");
            return;
        }

        string xmlPath   = config.Paths.Xml;      // e.g., "data/input.xml"
        string xsltPath  = config.Paths.Xslt;     // e.g., "templates/transform.xslt"
        string outputPdf = config.Paths.OutputPdf; // e.g., "output/result.pdf"

        // ------------------------------------------------------------
        // Validate input files existence
        // ------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        // ------------------------------------------------------------
        // Generate PDF using Aspose.Pdf core API (no Facades, no prohibited namespaces)
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            // BindXml applies the XSLT transformation and populates the PDF structure
            pdfDoc.BindXml(xmlPath, xsltPath);

            // Save the generated PDF; the target format is PDF by default
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated successfully at: {outputPdf}");
    }
}
using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Config
{
    public string XsltPath { get; set; }
}

class Program
{
    static void Main()
    {
        // Paths to the input XML and the configuration file.
        const string xmlInputPath = "input.xml";
        const string configPath   = "appsettings.json";
        const string pdfOutputPath = "output.pdf";

        // Validate required files.
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML input not found: {xmlInputPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration (expects a JSON like { "XsltPath": "transform.xslt" } ).
        Config cfg;
        try
        {
            string json = File.ReadAllText(configPath);
            cfg = JsonSerializer.Deserialize<Config>(json);
            if (cfg == null || string.IsNullOrWhiteSpace(cfg.XsltPath))
                throw new InvalidOperationException("XsltPath is missing in configuration.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Ensure the XSLT file exists.
        if (!File.Exists(cfg.XsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {cfg.XsltPath}");
            return;
        }

        // Load the XML and apply the XSLT using XmlLoadOptions.
        // XmlLoadOptions(string xslFile) constructor attaches the XSLT to the load operation.
        XmlLoadOptions loadOptions = new XmlLoadOptions(cfg.XsltPath);

        // Use a using block for deterministic disposal of the Document.
        using (Document pdfDoc = new Document(xmlInputPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF generated successfully at '{pdfOutputPath}'.");
    }
}
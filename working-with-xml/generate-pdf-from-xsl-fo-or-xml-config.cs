using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    // Configuration model matching the JSON structure.
    private class Config
    {
        public string XslFoFile { get; set; }      // Path to XSL-FO source (optional)
        public string XmlFile { get; set; }        // Path to XML source (optional)
        public string XslFile { get; set; }        // Path to XSLT file (optional, used with XML)
        public string OutputPdf { get; set; }      // Desired output PDF path
    }

    static void Main()
    {
        const string configPath = "config.json";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration.
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

        // Validate required fields.
        if (string.IsNullOrWhiteSpace(cfg.OutputPdf))
        {
            Console.Error.WriteLine("OutputPdf path must be specified in configuration.");
            return;
        }

        try
        {
            // CASE 1: XSL-FO source file is provided.
            if (!string.IsNullOrWhiteSpace(cfg.XslFoFile) && File.Exists(cfg.XslFoFile))
            {
                // Load XSL-FO file using XslFoLoadOptions (no XSLT needed).
                using (Document pdfDoc = new Document(cfg.XslFoFile, new XslFoLoadOptions()))
                {
                    pdfDoc.Save(cfg.OutputPdf); // Save as PDF.
                }

                Console.WriteLine($"PDF generated from XSL-FO: {cfg.OutputPdf}");
                return;
            }

            // CASE 2: XML source with optional XSLT transformation.
            if (!string.IsNullOrWhiteSpace(cfg.XmlFile) && File.Exists(cfg.XmlFile))
            {
                // If an XSLT file is supplied, use it; otherwise load XML directly.
                if (!string.IsNullOrWhiteSpace(cfg.XslFile) && File.Exists(cfg.XslFile))
                {
                    // Load XML + XSLT using XmlLoadOptions that accepts an XSL file.
                    using (Document pdfDoc = new Document(cfg.XmlFile, new XmlLoadOptions(cfg.XslFile)))
                    {
                        pdfDoc.Save(cfg.OutputPdf);
                    }

                    Console.WriteLine($"PDF generated from XML + XSLT: {cfg.OutputPdf}");
                }
                else
                {
                    // Load XML without XSLT (direct conversion if supported).
                    using (Document pdfDoc = new Document(cfg.XmlFile, new XmlLoadOptions()))
                    {
                        pdfDoc.Save(cfg.OutputPdf);
                    }

                    Console.WriteLine($"PDF generated from XML (no XSLT): {cfg.OutputPdf}");
                }

                return;
            }

            Console.Error.WriteLine("Neither a valid XSL-FO file nor an XML file was specified in the configuration.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF generation: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

namespace PdfGenerationWithConfig
{
    // Simple POCO to map configuration JSON
    public class GenerationConfig
    {
        public string XmlFilePath { get; set; }      // Path to the source XML
        public string XslFilePath { get; set; }      // Path to the XSLT file
        public string OutputPdfPath { get; set; }    // Desired output PDF path
    }

    class Program
    {
        static void Main()
        {
            // -----------------------------------------------------------------
            // 1. Load configuration from a JSON file (e.g., "config.json")
            // -----------------------------------------------------------------
            const string configPath = "config.json";

            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            GenerationConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<GenerationConfig>(json);
                if (config == null ||
                    string.IsNullOrWhiteSpace(config.XmlFilePath) ||
                    string.IsNullOrWhiteSpace(config.XslFilePath) ||
                    string.IsNullOrWhiteSpace(config.OutputPdfPath))
                {
                    Console.Error.WriteLine("Invalid configuration content.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // -----------------------------------------------------------------
            // 2. Verify that source files exist
            // -----------------------------------------------------------------
            if (!File.Exists(config.XmlFilePath))
            {
                Console.Error.WriteLine($"XML source file not found: {config.XmlFilePath}");
                return;
            }

            if (!File.Exists(config.XslFilePath))
            {
                Console.Error.WriteLine($"XSLT file not found: {config.XslFilePath}");
                return;
            }

            // -----------------------------------------------------------------
            // 3. Create XmlLoadOptions with the XSLT file path.
            //    This tells Aspose.Pdf to apply the XSLT transformation while loading.
            // -----------------------------------------------------------------
            XmlLoadOptions xmlLoadOptions = new XmlLoadOptions(config.XslFilePath);

            // Optional: if the XSLT expects parameters, set them via XsltArgumentList
            // Example:
            // System.Xml.Xsl.XsltArgumentList args = new System.Xml.Xsl.XsltArgumentList();
            // args.AddParam("title", "", "Sample Report");
            // xmlLoadOptions.XsltArgumentList = args;

            // -----------------------------------------------------------------
            // 4. Load the XML document (with transformation) and generate PDF.
            //    The Document constructor takes the XML file path and the load options.
            // -----------------------------------------------------------------
            try
            {
                using (Document pdfDocument = new Document(config.XmlFilePath, xmlLoadOptions))
                {
                    // -----------------------------------------------------------------
                    // 5. Save the resulting PDF to the configured output path.
                    //    No custom SaveOptions are required for plain PDF output.
                    // -----------------------------------------------------------------
                    pdfDocument.Save(config.OutputPdfPath);
                }

                Console.WriteLine($"PDF generated successfully: {config.OutputPdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"PDF generation failed: {ex.Message}");
            }
        }
    }
}
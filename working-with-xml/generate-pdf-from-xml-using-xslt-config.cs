using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // POCO classes that match the structure of appsettings.json
    private class Settings
    {
        public InputFilesSection InputFiles { get; set; }
        public string OutputFile { get; set; }
    }

    private class InputFilesSection
    {
        public string Xml { get; set; }
        public string Xslt { get; set; }
    }

    static void Main()
    {
        // Load configuration from appsettings.json using System.Text.Json (no external packages required)
        const string configFileName = "appsettings.json";
        if (!File.Exists(configFileName))
        {
            Console.Error.WriteLine($"Configuration file not found: {configFileName}");
            return;
        }

        Settings config;
        try
        {
            string json = File.ReadAllText(configFileName);
            config = JsonSerializer.Deserialize<Settings>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Retrieve the paths from the deserialized configuration
        string xmlInputPath = config?.InputFiles?.Xml;
        string xsltPath = config?.InputFiles?.Xslt;
        string outputPdfPath = config?.OutputFile;

        // Basic validation of the retrieved values
        if (string.IsNullOrWhiteSpace(xmlInputPath) || string.IsNullOrWhiteSpace(xsltPath) || string.IsNullOrWhiteSpace(outputPdfPath))
        {
            Console.Error.WriteLine("One or more configuration values are missing or empty.");
            return;
        }
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML input file not found: {xmlInputPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        // Create XmlLoadOptions with the XSLT file – this tells Aspose.Pdf to apply the XSLT during the XML → PDF conversion.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xsltPath);

        // Load the XML document using the options that contain the XSLT.
        using (Document pdfDoc = new Document(xmlInputPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{outputPdfPath}'.");
    }
}

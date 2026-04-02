using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

public class Config
{
    public string XmlFile { get; set; }
    public string XsltFile { get; set; }
    public string OutputPdf { get; set; }
}

public class Program
{
    public static void Main()
    {
        // Resolve the path of the configuration file placed beside the executable.
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: '{configPath}'." +
                                    " Ensure that 'config.json' exists next to the executable.");
            return; // Gracefully exit instead of throwing an unhandled exception.
        }

        // Read and deserialize the JSON configuration.
        string configJson = File.ReadAllText(configPath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        Config config = JsonSerializer.Deserialize<Config>(configJson, options);
        if (config == null)
        {
            Console.Error.WriteLine("Failed to deserialize configuration file. Check its JSON syntax.");
            return;
        }

        // Validate the required file paths.
        string xmlPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.XmlFile ?? string.Empty));
        string xsltPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.XsltFile ?? string.Empty));
        string outputPdfName = Path.GetFileName(config.OutputPdf ?? "output.pdf");
        string outputPdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputPdfName);

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML source file not found: '{xmlPath}'.");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: '{xsltPath}'.");
            return;
        }

        // Create XmlLoadOptions using the XSLT file path read from configuration.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xsltPath);

        // Convert the XML document to PDF using the loaded options.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the PDF. The file name must be a simple filename without a path.
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"PDF generated successfully: '{outputPdfPath}'.");
        }
    }
}

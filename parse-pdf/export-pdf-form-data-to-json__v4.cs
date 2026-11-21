using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Entry point: expects two arguments
    //   args[0] - path to the source PDF file
    //   args[1] - output format flag: "json" or "xml"
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ExportFormData <pdfPath> <format(json|xml)>");
            return;
        }

        string pdfPath = args[0];
        string format  = args[1].ToLowerInvariant();

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        // Determine output file name based on requested format
        string outputPath = Path.ChangeExtension(pdfPath,
            format == "json" ? ".json" :
            format == "xml"  ? ".xml"  :
            ".txt"); // fallback, should not happen

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Ensure the document contains a form
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Collect field name/value pairs
                var fieldData = new Dictionary<string, string>();
                foreach (Field field in doc.Form)
                {
                    // Some fields may have null values; treat them as empty strings
                    string value = field.Value?.ToString() ?? string.Empty;
                    fieldData[field.FullName] = value;
                }

                // Export according to the requested format
                if (format == "json")
                {
                    ExportToJson(fieldData, outputPath);
                }
                else if (format == "xml")
                {
                    ExportToXml(fieldData, outputPath);
                }
                else
                {
                    Console.Error.WriteLine($"Unsupported format: {format}. Use 'json' or 'xml'.");
                    return;
                }

                Console.WriteLine($"Form data exported to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }

    // Writes the dictionary as a pretty‑printed JSON file
    private static void ExportToJson(Dictionary<string, string> data, string path)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path, json);
    }

    // Writes the dictionary as a simple XML document:
    // <FormData><Field name="...">value</Field>...</FormData>
    private static void ExportToXml(Dictionary<string, string> data, string path)
    {
        XElement root = new XElement("FormData");
        foreach (var kvp in data)
        {
            XElement fieldElem = new XElement("Field",
                new XAttribute("name", kvp.Key),
                kvp.Value);
            root.Add(fieldElem);
        }

        XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        doc.Save(path);
    }
}
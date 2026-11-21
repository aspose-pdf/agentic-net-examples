using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;   // Facade namespace as required

class Program
{
    static void Main()
    {
        // List of PDF files whose JSON exports will be combined.
        // It is assumed that each PDF has a corresponding JSON file
        // with the same name and a ".json" extension.
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Collect individual JSON objects.
        List<string> jsonObjects = new List<string>();

        // PdfFileEditor does NOT implement IDisposable; instantiate once.
        PdfFileEditor editor = new PdfFileEditor();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF not found: {pdfPath}");
                continue;
            }

            // Determine the expected JSON export file name.
            string jsonPath = Path.ChangeExtension(pdfPath, ".json");

            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"JSON export not found for {pdfPath}");
                continue;
            }

            // Read the JSON content.
            string jsonContent = File.ReadAllText(jsonPath).Trim();

            // Validate that the content is a JSON object.
            if (jsonContent.StartsWith("{") && jsonContent.EndsWith("}"))
            {
                jsonObjects.Add(jsonContent);
            }
            else
            {
                Console.Error.WriteLine($"Invalid JSON format in {jsonPath}");
            }
        }

        // Combine all objects into a single JSON array.
        string combinedJson = "[" + string.Join(",", jsonObjects) + "]";

        // Write the combined array to the output file.
        string outputPath = "combined.json";
        File.WriteAllText(outputPath, combinedJson);
        Console.WriteLine($"Combined JSON written to {outputPath}");
    }
}
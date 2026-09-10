using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files whose JSON data will be combined
        string[] pdfFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // Collect each PDF's exported JSON object as a string
        List<string> jsonObjects = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            // Ensure the source PDF exists
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Use the Facades Form class to export form fields to JSON
            using (Form form = new Form(pdfPath))
            {
                using (MemoryStream jsonStream = new MemoryStream())
                {
                    // Export JSON (indented for readability)
                    form.ExportJson(jsonStream, true);
                    jsonStream.Position = 0; // Reset stream for reading

                    using (StreamReader reader = new StreamReader(jsonStream))
                    {
                        string json = reader.ReadToEnd().Trim();

                        // The exported JSON is an object (enclosed in { })
                        // Add it directly to the collection
                        jsonObjects.Add(json);
                    }
                }
            }
        }

        // Combine all JSON objects into a single JSON array
        string combinedJson = "[" + string.Join(",", jsonObjects) + "]";

        // Output file path
        string outputPath = "combined.json";

        // Write the combined JSON array to the file
        File.WriteAllText(outputPath, combinedJson);

        Console.WriteLine($"Combined JSON written to '{outputPath}'.");
    }
}
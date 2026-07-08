using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files whose form data will be exported to JSON
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output file that will contain a single JSON array
        const string outputJsonPath = "combined.json";

        var jsonObjects = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Bind the PDF to the Facades Form object
            using (Form form = new Form(pdfPath))
            {
                // Export the form fields to a memory stream as JSON
                using (MemoryStream ms = new MemoryStream())
                {
                    form.ExportJson(ms);               // ExportJson writes JSON to the stream
                    ms.Position = 0;                   // Reset stream position for reading

                    using (StreamReader reader = new StreamReader(ms))
                    {
                        string json = reader.ReadToEnd().Trim();
                        // The exported JSON represents an object; keep it as‑is for array aggregation
                        jsonObjects.Add(json);
                    }
                }
            }
        }

        // Combine all exported JSON objects into a single JSON array
        string combinedJson = "[" + string.Join(",", jsonObjects) + "]";

        // Write the combined JSON array to the output file
        File.WriteAllText(outputJsonPath, combinedJson);
        Console.WriteLine($"Combined JSON saved to '{outputJsonPath}'.");
    }
}
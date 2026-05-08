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

        // Collect individual JSON fragments
        List<string> jsonFragments = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Export form fields of the current PDF to a memory stream
            using (Form form = new Form(pdfPath))
            using (MemoryStream ms = new MemoryStream())
            {
                // ExportJson writes the JSON representation of the form fields
                form.ExportJson(ms, true);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms))
                {
                    string json = reader.ReadToEnd().Trim();

                    // If the exported JSON is an array, strip the surrounding brackets
                    if (json.StartsWith("[") && json.EndsWith("]"))
                    {
                        json = json.Substring(1, json.Length - 2).Trim();
                    }

                    // Add the fragment (object or comma‑separated items) to the list
                    if (!string.IsNullOrEmpty(json))
                    {
                        jsonFragments.Add(json);
                    }
                }
            }
        }

        // Combine all fragments into a single JSON array
        string combinedJson = "[" + string.Join(",", jsonFragments) + "]";

        // Write the combined JSON to the output file
        File.WriteAllText(outputJsonPath, combinedJson);
        Console.WriteLine($"Combined JSON saved to '{outputJsonPath}'.");
    }
}
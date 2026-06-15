using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files whose form data will be exported to JSON
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Output file that will contain a single JSON array
        const string outputJsonPath = "combined.json";

        // List to hold each PDF's exported JSON string
        List<string> jsonFragments = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Use the Form facade to export form fields to JSON
            using (Form form = new Form(pdfPath))
            using (MemoryStream ms = new MemoryStream())
            {
                // Export JSON (indented for readability)
                form.ExportJson(ms, true);
                ms.Position = 0;
                using (StreamReader reader = new StreamReader(ms))
                {
                    string json = reader.ReadToEnd().Trim();
                    // Ensure each fragment is a valid JSON object (remove surrounding whitespace)
                    jsonFragments.Add(json);
                }
            }
        }

        // Combine fragments into a single JSON array
        string combinedJson = "[" + string.Join(",", jsonFragments) + "]";

        // Write the combined JSON array to the output file
        File.WriteAllText(outputJsonPath, combinedJson);
        Console.WriteLine($"Combined JSON saved to '{outputJsonPath}'.");
    }
}
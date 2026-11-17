using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "annotations_summary.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Dictionary to hold page number (1‑based) and list of annotation type names
            var summary = new Dictionary<int, List<string>>();

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page indexes)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                var types = new List<string>();

                // Each page has an Annotations collection; iterate if it contains any items
                if (page.Annotations != null && page.Annotations.Count > 0)
                {
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Use the concrete .NET type name as the annotation type identifier
                        types.Add(annotation.GetType().Name);
                    }
                }

                summary[i] = types;
            }

            // Serialize the dictionary to JSON (indented for readability)
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(summary, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Annotation summary saved to '{outputJsonPath}'.");
    }
}
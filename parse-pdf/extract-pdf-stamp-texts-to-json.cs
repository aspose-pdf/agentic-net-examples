using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputJsonPath = "stamps.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // List to hold extracted stamp texts
        List<string> stampTexts = new List<string>();

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // StampAnnotation represents a PDF stamp
                    if (annotation is StampAnnotation stampAnno)
                    {
                        // The textual content of the stamp is stored in the Contents property
                        if (!string.IsNullOrEmpty(stampAnno.Contents))
                        {
                            stampTexts.Add(stampAnno.Contents);
                        }
                    }
                }
            }
        }

        // Serialize the list of strings to a JSON array
        string json = JsonSerializer.Serialize(stampTexts, new JsonSerializerOptions { WriteIndented = true });

        // Write JSON to file
        File.WriteAllText(outputJsonPath, json);

        Console.WriteLine($"Extracted {stampTexts.Count} stamp text(s) to '{outputJsonPath}'.");
    }
}
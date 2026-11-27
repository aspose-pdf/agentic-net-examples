using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputJsonPath = "pages.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare a list to hold page dimension info
            var pagesInfo = new List<object>();

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page dimensions are expressed in points (1 point = 1/72 inch)
                double width  = page.PageInfo.Width;
                double height = page.PageInfo.Height;

                pagesInfo.Add(new
                {
                    PageNumber = i,
                    Width      = width,
                    Height     = height
                });
            }

            // Serialize the list to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(pagesInfo, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Page dimensions written to '{outputJsonPath}'.");
    }
}
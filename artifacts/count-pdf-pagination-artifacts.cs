using System;
using System.IO;
using Aspose.Pdf;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "pagination_count.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF and count pagination artifacts (pages)
        int paginationCount;
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; doc.Pages.Count gives total pages
            paginationCount = doc.Pages.Count;
        }

        // Create a simple JSON object with the count
        var jsonObject = new { PaginationArtifacts = paginationCount };
        string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON to a file
        File.WriteAllText(outputJsonPath, json);
        Console.WriteLine($"Pagination count saved to '{outputJsonPath}'.");
    }
}
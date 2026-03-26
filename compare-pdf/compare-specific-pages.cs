using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string jsonReportPath = "diffReport.json";

        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load the two PDF documents
        using (Document firstDoc = new Document(firstPdfPath))
        using (Document secondDoc = new Document(secondPdfPath))
        {
            // Define the pages we want to compare (1‑based indexing)
            int[] pagesToCompare = new int[] { 1, 3, 5 };

            // Perform a full document comparison (page‑by‑page)
            ComparisonOptions options = new ComparisonOptions();
            List<List<DiffOperation>> allDiffs = TextPdfComparer.CompareDocumentsPageByPage(firstDoc, secondDoc, options);

            // Filter the results to the selected pages
            List<List<DiffOperation>> selectedDiffs = new List<List<DiffOperation>>();
            foreach (int pageNumber in pagesToCompare)
            {
                // Ensure the page number exists in the result list
                if (pageNumber >= 1 && pageNumber <= allDiffs.Count)
                {
                    selectedDiffs.Add(allDiffs[pageNumber - 1]); // zero‑based list index
                }
                else
                {
                    Console.Error.WriteLine($"Page {pageNumber} is out of range for the compared documents.");
                }
            }

            // Generate a JSON diff report for the selected pages
            JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
            jsonGenerator.GenerateOutput(selectedDiffs, jsonReportPath);

            Console.WriteLine($"Diff report for pages {string.Join(", ", pagesToCompare)} saved to '{jsonReportPath}'.");
        }
    }
}
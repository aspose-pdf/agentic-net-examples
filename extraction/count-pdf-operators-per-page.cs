using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "operator_counts.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Prepare CSV writer
                using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header
                    csvWriter.WriteLine("PageNumber,OperatorCount");

                    // Pages are 1‑based in Aspose.Pdf
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // The low‑level operators are stored in the page's Contents collection
                        int operatorCount = page.Contents.Count;

                        // Write the count for the current page
                        csvWriter.WriteLine($"{pageIndex},{operatorCount}");
                    }
                }
            }

            Console.WriteLine($"Operator counts written to '{outputCsvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
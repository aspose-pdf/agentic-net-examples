using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Calculate the 1‑based midpoint position
                int insertPosition = (doc.Pages.Count / 2) + 1;

                // Insert ten empty pages at the calculated position
                for (int i = 0; i < 10; i++)
                {
                    doc.Pages.Insert(insertPosition);
                }

                // Save the modified document as PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully inserted 10 pages at the midpoint. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
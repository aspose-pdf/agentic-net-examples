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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Delete the third page (pages are 1‑based)
                if (doc.Pages.Count >= 3)
                {
                    doc.Pages.Delete(3);
                }
                else
                {
                    Console.WriteLine("Document has fewer than 3 pages; no page deleted.");
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Third page removed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
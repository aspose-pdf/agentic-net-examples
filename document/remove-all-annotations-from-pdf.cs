using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_clean.pdf";

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
                // Pages are 1‑based; iterate through each page
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Delete all annotations on the current page
                    doc.Pages[i].Annotations.Delete();
                }

                // Save the cleaned PDF (no SaveOptions needed for PDF output)
                doc.Save(outputPath);
            }

            Console.WriteLine($"All annotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
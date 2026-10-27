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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdf = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= pdf.Pages.Count; i++)
            {
                Page page = pdf.Pages[i];

                // If the page width exceeds 600 points, resize it to A4 (595 x 842 points)
                if (page.PageInfo.Width > 600)
                {
                    page.PageInfo.Width = 595; // A4 width in points
                    page.PageInfo.Height = 842; // A4 height in points
                }
            }

            // Save the modified document (PDF format)
            pdf.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
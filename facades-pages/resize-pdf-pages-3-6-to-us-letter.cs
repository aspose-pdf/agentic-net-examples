using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Pages are 1‑based; adjust pages 3 through 6
            for (int pageNumber = 3; pageNumber <= 6; pageNumber++)
            {
                // Ensure the page exists
                if (pageNumber > editor.Document.Pages.Count)
                    break;

                // Set the page size to US Letter (279 x 216 points)
                editor.Document.Pages[pageNumber].SetPageSize(
                    Aspose.Pdf.PageSize.PageLetter.Width,
                    Aspose.Pdf.PageSize.PageLetter.Height);
            }

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 resized to Letter and saved as '{outputPath}'.");
    }
}
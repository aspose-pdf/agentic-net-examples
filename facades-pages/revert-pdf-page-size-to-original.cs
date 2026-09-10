using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use PdfPageEditor to obtain the original size of page 8
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);
                PageSize originalSize = editor.GetPageSize(8); // pages are 1‑based
                double originalWidth  = originalSize.Width;
                double originalHeight = originalSize.Height;

                // -----------------------------------------------------------------
                // At this point the page size could have been changed elsewhere.
                // For demonstration we change it to a different size first.
                // -----------------------------------------------------------------
                doc.Pages[8].SetPageSize(500, 700); // arbitrary new size

                // Revert page 8 back to its original dimensions
                doc.Pages[8].SetPageSize(originalWidth, originalHeight);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 8 size reverted and saved to '{outputPath}'.");
    }
}
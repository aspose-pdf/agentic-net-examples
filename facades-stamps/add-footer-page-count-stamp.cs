using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize PdfFileStamp facade
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF
            fileStamp.BindPdf(inputPath);

            // Add a footer that shows the total page count.
            // The placeholder {page_count} is replaced with the total number of pages.
            // AddPageNumber places the text at the bottom centre of each page.
            fileStamp.AddPageNumber("{page_count}");

            // Save the result
            fileStamp.Save(outputPath);

            // Close the facade (optional, Dispose will also close)
            fileStamp.Close();
        }

        Console.WriteLine($"Footer stamp added. Output saved to '{outputPath}'.");
    }
}
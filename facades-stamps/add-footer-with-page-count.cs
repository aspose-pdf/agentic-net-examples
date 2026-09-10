using System;
using System.IO;
using Aspose.Pdf;
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

        // PdfFileStamp does not implement IDisposable, so we do not use a using block.
        // Use the parameterless constructor and bind the source PDF via BindPdf().
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Add a footer that shows the current page number and total page count.
        // The placeholder "#" is replaced with the current page number.
        // The placeholder "{page_count}" is replaced with the total number of pages.
        // Position the footer at the bottom middle of each page.
        fileStamp.AddPageNumber("Page # of {page_count}", PdfFileStamp.PosBottomMiddle);

        // Save the result to the specified output file.
        fileStamp.Save(outputPath);

        // Close the facade to release resources.
        fileStamp.Close();

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine total number of pages in the source PDF.
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count; // 1‑based page count
        }

        // Build an array containing only even page numbers.
        int evenCount = pageCount / 2;
        int[] evenPages = new int[evenCount];
        int idx = 0;
        for (int i = 2; i <= pageCount; i += 2)
        {
            evenPages[idx++] = i;
        }

        // Configure PdfFileStamp facade.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPath;
        fileStamp.OutputFile = outputPath;

        // Create a stamp (e.g., a simple text stamp) and assign even pages.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        // Example: bind a text logo as the stamp content.
        stamp.BindLogo(new FormattedText("CONFIDENTIAL", System.Drawing.Color.Red, "Helvetica", EncodingType.Winansi, false, 48));
        stamp.IsBackground = true; // place stamp behind page content
        stamp.Pages = evenPages;   // apply only to even pages

        // Add the stamp to the document and finalize.
        fileStamp.AddStamp(stamp);
        fileStamp.Close(); // saves the output file

        Console.WriteLine($"Stamp applied to even pages. Output saved to '{outputPath}'.");
    }
}
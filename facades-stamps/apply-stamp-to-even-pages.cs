using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string stampPdf   = "stamp.pdf";   // PDF file whose first page will be used as stamp
        const string outputPath = "stamped_even_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampPdf))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp file not found: {stampPdf}");
            return;
        }

        // Determine the even page numbers of the source document
        int[] evenPages;
        using (Document srcDoc = new Document(inputPath))
        {
            evenPages = Enumerable.Range(1, srcDoc.Pages.Count)
                                  .Where(p => p % 2 == 0)
                                  .ToArray();
        }

        // Create a stamp that uses the first page of the stamp PDF
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(stampPdf, 1);          // use page 1 of stampPdf as the stamp content
        stamp.IsBackground = true;          // place stamp behind existing content
        stamp.Pages = evenPages;             // apply only to even pages

        // Apply the stamp to the source PDF using PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPath;
        fileStamp.OutputFile = outputPath;
        fileStamp.AddStamp(stamp);
        fileStamp.Close();                   // writes the output file

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp applied to even pages. Output saved to '{outputPath}'.");
    }
}
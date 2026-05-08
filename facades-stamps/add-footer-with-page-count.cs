using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF with footer.
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the input file exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PdfFileStamp facade and bind it to the source PDF.
        // PdfFileStamp implements IDisposable via SaveableFacade, so we use a using block.
        using (PdfFileStamp stampFacade = new PdfFileStamp())
        {
            // Bind the source PDF file.
            stampFacade.BindPdf(inputPdf);

            // Create a FormattedText object that contains the placeholder for total page count.
            // The placeholder {page_count} is recognized by Aspose.Pdf and will be replaced
            // with the actual number of pages when the document is saved.
            FormattedText footerText = new FormattedText("Page {page_count}");

            // Add the footer to all pages. The second argument is the bottom margin (in points).
            stampFacade.AddFooter(footerText, 10f);

            // Save the modified PDF to the output path.
            stampFacade.Save(outputPdf);
        }

        Console.WriteLine($"Footer with page count added successfully: {outputPdf}");
    }
}
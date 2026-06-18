using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Load the source PDF (required for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // No modifications needed on the Document itself.
        }

        // Initialize the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPath;
        fileStamp.OutputFile = outputPath;

        // Create formatted text for the footer (e.g., a static label)
        FormattedText footerText = new FormattedText(
            "Confidential Document",          // text
            System.Drawing.Color.Gray,        // text color (System.Drawing.Color is required here)
            "Helvetica",                      // font name
            EncodingType.Winansi,             // encoding
            false,                            // embed font flag
            10);                              // font size

        // Add the footer text with a bottom margin of 20 points
        fileStamp.AddFooter(footerText, 20);

        // Add page numbering. The '#' placeholder is replaced with the current page number.
        // The '{page_count}' placeholder is also recognized by Aspose.Pdf for total pages.
        fileStamp.AddPageNumber("Page # of {page_count}");

        // Finalize and write the output PDF
        fileStamp.Close();

        Console.WriteLine($"Footer with automatic page count added: {outputPath}");
    }
}
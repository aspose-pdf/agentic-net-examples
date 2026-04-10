using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for FormattedText and EncodingType

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

        // Initialize the PdfFileStamp facade and bind the source PDF.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create formatted text for the footer.
        // The placeholders {page_number} and {page_count} will be replaced automatically.
        FormattedText footer = new FormattedText(
            "Page {page_number} of {page_count}",
            System.Drawing.Color.Gray,
            "Helvetica",
            EncodingType.Winansi,
            false,
            10); // font size

        // Add the footer with a bottom margin of 20 units.
        fileStamp.AddFooter(footer, 20);

        // Save the stamped PDF and close the facade.
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Footer stamp added and saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using System.Drawing; // Required for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Initialize the PdfFileStamp facade
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Load the source PDF
            fileStamp.BindPdf(inputPath);

            // Create formatted text with the {file_name} placeholder
            // Use System.Drawing.Color for the color argument and a float for the font size
            FormattedText header = new FormattedText(
                "{file_name}",                // placeholder for the PDF file name
                System.Drawing.Color.Black,   // text color (System.Drawing.Color)
                "Helvetica",                 // font name
                EncodingType.Winansi,         // encoding
                false,                        // embed font flag
                12f);                         // font size (float)

            // Add the header to each page (top margin of 20 points)
            fileStamp.AddHeader(header, 20f);

            // Save the resulting PDF
            fileStamp.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Initialize the PdfFileStamp facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);

            // Create formatted text containing the {file_name} placeholder
            FormattedText headerText = new FormattedText(
                "{file_name}",
                System.Drawing.Color.Black,
                "Helvetica",
                EncodingType.Winansi,
                false,
                12);

            // Add the header to each page; second argument is the vertical offset from the top
            fileStamp.AddHeader(headerText, 20f);

            // Save the stamped PDF
            fileStamp.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
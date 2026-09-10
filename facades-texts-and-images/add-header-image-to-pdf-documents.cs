using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least a header image path and one PDF file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: app.exe <headerImagePath> <pdfPath1> [<pdfPath2> ...]");
            return;
        }

        string headerImagePath = args[0];
        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // Process each PDF file supplied on the command line
        for (int i = 1; i < args.Length; i++)
        {
            string inputPdfPath = args[i];
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
                continue;
            }

            // Create an output file name by appending "_header" before the extension
            string outputPdfPath = Path.Combine(
                Path.GetDirectoryName(inputPdfPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPdfPath) + "_header.pdf");

            // Use PdfFileStamp to add the header image
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPdfPath);               // Load the source PDF
            fileStamp.AddHeader(headerImagePath, 50f);     // Add image as header with a top margin of 50 units
            fileStamp.Save(outputPdfPath);                 // Write the modified PDF to the output path
            fileStamp.Close();                             // Release resources

            Console.WriteLine($"Processed: {inputPdfPath} → {outputPdfPath}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: header image path and one or more PDF files
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <app> <headerImagePath> <pdfPath1> [pdfPath2] ...");
            return;
        }

        string headerImagePath = args[0];

        // Verify the header image exists once before processing
        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // Process each PDF file supplied in the arguments
        for (int i = 1; i < args.Length; i++)
        {
            string inputPdfPath = args[i];

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                continue;
            }

            // Build output file name by appending "_header" before the extension
            string outputPdfPath = Path.Combine(
                Path.GetDirectoryName(inputPdfPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPdfPath) + "_header.pdf");

            // Use PdfFileStamp facade to add the header image
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPdfPath);                     // Load the source PDF
            fileStamp.AddHeader(headerImagePath, 20f);           // Add image as header with 20 units top margin
            fileStamp.Save(outputPdfPath);                       // Save the modified PDF
            fileStamp.Close();                                   // Release resources

            Console.WriteLine($"Processed: {inputPdfPath} -> {outputPdfPath}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: header image path followed by one or more PDF files.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <exe> <headerImagePath> <pdfPath1> [<pdfPath2> ...]");
            return;
        }

        string headerImagePath = args[0];

        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // Process each PDF file supplied in the arguments.
        for (int i = 1; i < args.Length; i++)
        {
            string inputPdf = args[i];

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"PDF not found: {inputPdf}");
                continue;
            }

            // Build an output file name by appending "_header" before the extension.
            string outputPdf = Path.Combine(
                Path.GetDirectoryName(inputPdf) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPdf) + "_header.pdf");

            try
            {
                // Initialize the facade and bind the source PDF.
                PdfFileStamp fileStamp = new PdfFileStamp();
                fileStamp.BindPdf(inputPdf);

                // Add the header image to every page. Top margin is set to 20 points.
                fileStamp.AddHeader(headerImagePath, 20f);

                // Save the modified PDF to the output path.
                fileStamp.Save(outputPdf);

                // Close the facade to release resources.
                fileStamp.Close();

                Console.WriteLine($"Header added: {outputPdf}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPdf}': {ex.Message}");
            }
        }
    }
}
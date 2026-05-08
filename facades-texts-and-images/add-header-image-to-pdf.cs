using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect first argument to be the header image file,
        // remaining arguments are PDF files to process.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <headerImagePath> <pdfPath1> [<pdfPath2> ...]");
            return;
        }

        string headerImagePath = args[0];
        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // Process each PDF file.
        for (int i = 1; i < args.Length; i++)
        {
            string inputPdf = args[i];
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"PDF not found: {inputPdf}");
                continue;
            }

            // Create output file name by inserting "_header" before the extension.
            string outputPdf = Path.Combine(
                Path.GetDirectoryName(inputPdf) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPdf) + "_header.pdf");

            try
            {
                // Use PdfFileStamp facade to add a header image.
                using (PdfFileStamp fileStamp = new PdfFileStamp())
                {
                    // Bind the source PDF.
                    fileStamp.BindPdf(inputPdf);

                    // Add the header image. Top margin is set to 50 units.
                    fileStamp.AddHeader(headerImagePath, 50f);

                    // Save the modified PDF to the output path.
                    fileStamp.Save(outputPdf);

                    // Close the facade (releases resources).
                    fileStamp.Close();
                }

                Console.WriteLine($"Processed '{inputPdf}' -> '{outputPdf}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPdf}': {ex.Message}");
            }
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Create a PdfFileMend facade to work with the document
            using (PdfFileMend pdfMend = new PdfFileMend())
            {
                // Load the existing PDF into the facade
                pdfMend.BindPdf(inputPath);

                // (Optional) Perform modifications here, e.g., add images, text, etc.

                // Save the updated PDF using the facade's Save method
                pdfMend.Save(outputPath);
            }

            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
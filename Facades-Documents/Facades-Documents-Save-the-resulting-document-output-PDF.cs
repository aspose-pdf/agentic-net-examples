using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade that works with PDF documents
            using (PdfFileMend pdfMend = new PdfFileMend())
            {
                // Bind the existing PDF file to the facade
                pdfMend.BindPdf(inputPath);

                // (Optional) Perform operations on the document here
                // e.g., pdfMend.AddImage(...);

                // Save the resulting PDF document to the specified file
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
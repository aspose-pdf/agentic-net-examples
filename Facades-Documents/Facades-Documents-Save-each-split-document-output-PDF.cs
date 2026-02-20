using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Directory where split PDFs will be saved
        const string outputDirectory = "output";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the source PDF using a Facade (PdfFileMend)
            PdfFileMend pdfMend = new PdfFileMend();
            pdfMend.BindPdf(inputPdfPath);
            Document sourceDocument = pdfMend.Document;

            int totalPages = sourceDocument.Pages.Count;

            // Iterate through each page and create a separate PDF
            for (int pageIndex = 1; pageIndex <= totalPages; pageIndex++)
            {
                // Create a new empty document
                Document splitDocument = new Document();

                // Remove the default empty page that a new Document contains
                if (splitDocument.Pages.Count > 0)
                {
                    splitDocument.Pages.Delete(1);
                }

                // Add the current page from the source document
                splitDocument.Pages.Add(sourceDocument.Pages[pageIndex]);

                // Build output file name
                string outputPath = Path.Combine(outputDirectory, $"page_{pageIndex}.pdf");

                // Save the split document (using the provided document-save rule)
                splitDocument.Save(outputPath);

                Console.WriteLine($"Page {pageIndex} saved to: {outputPath}");
            }

            // Close the facade to release resources
            pdfMend.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // ---------- Remove metadata ----------
            // Clear standard document information
            pdfDocument.Info.Title = string.Empty;
            pdfDocument.Info.Author = string.Empty;
            pdfDocument.Info.Subject = string.Empty;
            pdfDocument.Info.Keywords = string.Empty;
            pdfDocument.Info.Creator = string.Empty;
            pdfDocument.Info.Producer = string.Empty;
            pdfDocument.Info.CreationDate = DateTime.MinValue;
            // Correct property name for the modification date
            pdfDocument.Info.ModDate = DateTime.MinValue;

            // Clear XMP metadata if present
            if (pdfDocument.Metadata != null && pdfDocument.Metadata.Count > 0)
            {
                pdfDocument.Metadata.Clear();
            }

            // ---------- Remove hidden layers (Optional Content Groups) ----------
            // The OptionalContent API is not available in the current Aspose.Pdf version used.
            // If you upgrade to a version that provides this API, uncomment the block below.
            // if (pdfDocument.OptionalContent != null && pdfDocument.OptionalContent.Groups != null)
            // {
            //     pdfDocument.OptionalContent.Groups.Clear();
            // }

            // Save the sanitized PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Sanitized PDF saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

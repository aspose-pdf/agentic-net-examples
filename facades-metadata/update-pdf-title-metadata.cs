using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle = "Updated PDF Title";

        // Ensure the source PDF exists; create a minimal one if it does not.
        if (!File.Exists(inputPath))
        {
            var doc = new Document();
            doc.Pages.Add(); // add a blank page so the file is a valid PDF
            doc.Save(inputPath);
        }

        // Initialize PdfFileInfo with the source PDF. Wrap in a using block to guarantee disposal.
        using (var pdfInfo = new PdfFileInfo(inputPath))
        {
            // Set the Title metadata.
            pdfInfo.Title = newTitle;

            // Save the updated PDF to a new file.
            bool success = pdfInfo.SaveNewInfo(outputPath);
            if (!success)
            {
                Console.Error.WriteLine("Failed to save updated PDF.");
            }
            else
            {
                Console.WriteLine($"Title updated and saved to '{outputPath}'.");
            }
            // No explicit Close() call is required; the using statement disposes the object.
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle   = "Updated PDF Title";

        // Ensure the source PDF exists – PdfFileInfo requires an existing file.
        if (!File.Exists(inputPath))
        {
            // Create a minimal PDF document so that PdfFileInfo can operate on it.
            var doc = new Document();
            doc.Pages.Add(); // add a blank page
            doc.Save(inputPath);
        }

        // Load the PDF file into the PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update the Title metadata
            pdfInfo.Title = newTitle;

            // Persist the changes to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Title updated successfully. Saved as '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}
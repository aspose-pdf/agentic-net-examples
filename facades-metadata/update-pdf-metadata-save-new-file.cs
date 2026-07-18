using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: The file '{inputPath}' was not found.");
            return;
        }

        // Initialise PdfFileInfo using the parameter‑less constructor and bind the PDF.
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            pdfInfo.BindPdf(inputPath);

            // Update metadata properties
            pdfInfo.Title    = "Updated Document Title";
            pdfInfo.Author   = "Jane Smith";
            pdfInfo.Subject  = "Metadata Update Example";
            pdfInfo.Keywords = "Aspose, PDF, Metadata";

            // Save the PDF with the updated metadata
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved ? "Metadata updated and saved successfully." : "Failed to save updated metadata.");
        }
    }
}

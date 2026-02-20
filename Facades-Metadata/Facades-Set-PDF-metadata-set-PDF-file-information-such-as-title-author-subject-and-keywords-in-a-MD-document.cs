using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF and the output PDF
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Create a PdfFileInfo facade and bind it to the existing PDF
            PdfFileInfo pdfInfo = new PdfFileInfo();
            pdfInfo.BindPdf(inputPath);

            // Set the desired metadata properties
            pdfInfo.Title = "Sample Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Sample Subject";
            pdfInfo.Keywords = "Aspose, PDF, Metadata";

            // Save the PDF with the updated metadata
            pdfInfo.Save(outputPath);

            Console.WriteLine($"Metadata successfully updated. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
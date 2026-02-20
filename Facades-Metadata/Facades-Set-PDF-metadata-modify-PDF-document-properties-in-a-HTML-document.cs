using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF with updated metadata
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Create a PdfFileInfo facade and bind it to the source PDF
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                pdfInfo.BindPdf(inputPdfPath);

                // Modify desired metadata properties
                pdfInfo.Title = "Sample PDF Title";
                pdfInfo.Author = "John Doe";
                pdfInfo.Subject = "Demonstration of metadata update using Aspose.Pdf.Facades";
                pdfInfo.Keywords = "Aspose.Pdf, metadata, example";

                // Save the PDF with the updated metadata to a new file
                pdfInfo.Save(outputPdfPath);
            }

            Console.WriteLine($"Metadata updated successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while updating PDF metadata: {ex.Message}");
        }
    }
}
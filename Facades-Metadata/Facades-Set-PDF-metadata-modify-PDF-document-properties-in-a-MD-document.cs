using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF and the output PDF with updated metadata
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Initialize the PdfFileInfo facade and bind it to the source PDF
            PdfFileInfo pdfInfo = new PdfFileInfo();
            pdfInfo.BindPdf(inputPdfPath);

            // Set desired metadata properties (Producer is read‑only and cannot be set)
            pdfInfo.Title = "Sample PDF Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demonstration of setting PDF metadata using Aspose.Pdf.Facades";
            pdfInfo.Keywords = "Aspose.Pdf, Metadata, Facades";
            pdfInfo.Creator = "My Application";
            // pdfInfo.Producer = "Aspose.Pdf"; // <-- removed because Producer is read‑only

            // Save the PDF with the updated metadata to a new file
            pdfInfo.Save(outputPdfPath);
            pdfInfo.Close();

            Console.WriteLine($"Metadata successfully updated. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while updating PDF metadata: {ex.Message}");
        }
    }
}

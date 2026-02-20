using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Create a PdfFileInfo facade and bind it to the existing PDF
            var pdfInfo = new PdfFileInfo();
            pdfInfo.BindPdf(inputPdfPath);

            // Set desired metadata properties
            pdfInfo.Title = "Sample PDF Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demonstration of metadata setting via Aspose.Pdf.Facades";
            pdfInfo.Keywords = "Aspose.Pdf, Facades, Metadata";
            pdfInfo.Creator = "My Application";

            // PDF date strings must follow the PDF date format (e.g., "D:20240220120000")
            string pdfDate = $"D:{DateTime.Now:yyyyMMddHHmmss}";
            pdfInfo.CreationDate = pdfDate;
            pdfInfo.ModDate = pdfDate;

            // Save the modified PDF to a new file
            pdfInfo.Save(outputPdfPath);
            Console.WriteLine($"Metadata updated and PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

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
            // Load the PDF document using the Facade class
            PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath);

            // Set desired metadata properties
            pdfInfo.Title = "Sample PDF Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demonstration of metadata setting";
            pdfInfo.Keywords = "Aspose.Pdf, Metadata, Example";
            pdfInfo.Creator = "Aspose.Pdf.Facades Sample";

            // PdfFileInfo expects string values for dates. Convert DateTime to PDF‑compatible string.
            string pdfDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            pdfInfo.CreationDate = pdfDate;
            pdfInfo.ModDate = pdfDate;

            // Save the updated PDF to a new file
            pdfInfo.Save(outputPdfPath);

            Console.WriteLine($"Metadata updated successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

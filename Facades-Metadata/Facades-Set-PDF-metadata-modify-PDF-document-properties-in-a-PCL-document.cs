using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF (or PCL converted to PDF) and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the Facade class PdfFileInfo
            PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

            // Set desired metadata properties
            pdfInfo.Title = "Sample PDF Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demonstration of metadata setting via Aspose.Pdf.Facades";
            pdfInfo.Keywords = "Aspose.Pdf, Facades, Metadata";
            pdfInfo.Creator = "My Application";

            // Set creation and modification dates (optional)
            // PdfFileInfo expects string values, so convert DateTime to PDF‑compatible string format
            string pdfDate = DateTime.Now.ToString("yyyyMMddHHmmss"); // Simple PDF date format
            pdfInfo.CreationDate = pdfDate;
            pdfInfo.ModDate = pdfDate;

            // Save the updated PDF to a new file
            pdfInfo.Save(outputPath);

            Console.WriteLine($"Metadata updated successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

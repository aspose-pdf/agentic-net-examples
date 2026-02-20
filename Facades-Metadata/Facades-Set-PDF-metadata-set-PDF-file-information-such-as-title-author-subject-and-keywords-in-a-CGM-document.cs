using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input CGM file and output PDF file paths
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        // Verify that the CGM source file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{cgmPath}'.");
            return;
        }

        try
        {
            // Convert CGM to PDF using the Facade PdfProducer
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, pdfPath);

            // Open the generated PDF with PdfFileInfo to set metadata
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Set desired metadata properties
            pdfInfo.Title = "Sample CGM Conversion";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Conversion of CGM to PDF";
            pdfInfo.Keywords = "CGM, PDF, Aspose.Pdf, Metadata";

            // Save the updated PDF (overwrites the existing file)
            pdfInfo.Save(pdfPath);

            Console.WriteLine($"PDF metadata set successfully. File saved at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
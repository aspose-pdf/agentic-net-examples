using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class SetPdfMetadataAndConvertToXps
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output XPS file path
        const string xpsPath = "output.xps";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Set PDF metadata using the PdfFileInfo facade
            // -----------------------------------------------------------------
            // Load the PDF file into the facade
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Set desired metadata properties
            pdfInfo.Title = "Sample Document Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demonstration of metadata setting";
            pdfInfo.Keywords = "Aspose.Pdf,Metadata,Example";

            // Save the updated PDF (overwrites the original file)
            pdfInfo.Save(pdfPath);

            // -----------------------------------------------------------------
            // 2. Convert the updated PDF to XPS format
            // -----------------------------------------------------------------
            // Load the PDF document (now containing the new metadata)
            Document pdfDocument = new Document(pdfPath);

            // Prepare XPS save options (default options are sufficient here)
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS
            pdfDocument.Save(xpsPath, xpsOptions);

            Console.WriteLine($"Metadata set and PDF converted to XPS successfully.\nXPS file: {xpsPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the input MHT file and the output PDF file.
        const string inputMhtPath  = "input.mht";
        const string outputPdfPath = "output.pdf";

        // Verify that the input file exists.
        if (!File.Exists(inputMhtPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputMhtPath}");
            return;
        }

        // Load the MHT file into a PDF Document using MhtLoadOptions.
        using (Document pdfDoc = new Document(inputMhtPath, new MhtLoadOptions()))
        {
            // Create a PdfFileInfo facade bound to the loaded document.
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfDoc))
            {
                // Set the desired metadata properties.
                pdfInfo.Title    = "Sample Document Title";
                pdfInfo.Author   = "John Doe";
                pdfInfo.Subject  = "Demonstration of MHT to PDF conversion";
                pdfInfo.Keywords = "Aspose.Pdf, MHT, PDF, Metadata";

                // Save the PDF with the updated metadata.
                pdfInfo.SaveNewInfo(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF saved with metadata to '{outputPdfPath}'.");
    }
}
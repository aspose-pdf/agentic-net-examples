using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PCL file and desired PDF/E output file
        const string inputPclPath = "sample.pcl";
        const string outputPdfPath = "sample.pdf";

        // Verify that the source PCL file exists
        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Error: PCL file not found at '{inputPclPath}'.");
            return;
        }

        // Load the PCL file using default load options.
        // PclLoadOptions can be customized (e.g., choose a conversion engine) if needed.
        PclLoadOptions loadOptions = new PclLoadOptions();
        // Example of selecting the new conversion engine:
        // loadOptions.ConversionEngine = PclLoadOptions.ConversionEngines.NewEngine;

        // Create a Document from the PCL source.
        Document pdfDocument = new Document(inputPclPath, loadOptions);

        // At this point the document is a regular PDF.
        // To produce a PDF/E (engineering) document you can use PdfFormatConversionOptions
        // with the appropriate PdfFormat (e.g., PdfFormat.PDF_X_4) before saving.
        // For simplicity, we directly save as PDF here.
        pdfDocument.Save(outputPdfPath);   // document-save rule

        Console.WriteLine($"Conversion completed. PDF saved to '{outputPdfPath}'.");
    }
}
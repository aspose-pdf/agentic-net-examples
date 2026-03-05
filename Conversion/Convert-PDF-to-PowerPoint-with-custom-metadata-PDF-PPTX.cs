using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including PptxSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // ----- Set custom metadata -----
                // Standard PDF metadata properties are available via the Info object
                pdfDocument.Info.Title   = "Custom Presentation Title";
                pdfDocument.Info.Author  = "John Doe";
                pdfDocument.Info.Subject = "Converted from PDF to PPTX";
                pdfDocument.Info.Keywords = "PDF,PowerPoint,Conversion";

                // ----- Prepare PPTX save options -----
                // PptxSaveOptions resides in the Aspose.Pdf namespace (no separate Saving namespace)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // ----- Save the document as PPTX -----
                // Use the overload that accepts a SaveOptions instance to ensure non‑PDF output
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading, metadata setting, or saving
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
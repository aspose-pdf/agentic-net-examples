using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, PptxSaveOptions, etc.)

class PdfToPptxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath  = "input.pdf";

        // Output PPTX file path
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF, set custom metadata, and save as PPTX
        try
        {
            // Document implements IDisposable – wrap in using for deterministic cleanup
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // ----- Custom metadata -----
                // Standard PDF document info properties (Title, Author, Subject, Keywords)
                pdfDoc.Info.Title    = "Quarterly Business Review";
                pdfDoc.Info.Author   = "Jane Smith";
                pdfDoc.Info.Subject  = "Conversion from PDF to PowerPoint";
                pdfDoc.Info.Keywords = "PDF, PPTX, conversion, custom metadata";

                // Optionally, you can also set XMP metadata if needed:
                // using (MemoryStream xmpStream = new MemoryStream())
                // {
                //     // Write XMP XML to the stream here...
                //     pdfDoc.SetXmpMetadata(xmpStream);
                // }

                // ----- Save as PPTX -----
                // When saving to a non‑PDF format you must provide explicit SaveOptions.
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the document as a PowerPoint presentation
                pdfDoc.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful. PPTX saved to '{outputPptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;               // Core API
// DocSaveOptions is in the Aspose.Pdf namespace (no separate Saving namespace)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDoc = "output.doc";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure save options for DOC conversion
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Use the Textbox recognition mode – fast and preserves layout,
                // which is suitable when we only need the images from the PDF.
                Mode = DocSaveOptions.RecognitionMode.Textbox,

                // Optional: if the source PDF contains OCR sub‑layers and we want
                // to extract the underlying images only, enable this flag.
                ExtractOcrSublayerOnly = true,

                // Set the output format explicitly (DOC, not DOCX)
                Format = DocSaveOptions.DocFormat.Doc
            };

            // Save the document as DOC using the configured options
            pdfDoc.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
    }
}
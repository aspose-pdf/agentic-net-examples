using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions classes are in the Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "scanned.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Perform OCR on scanned pages.
                // The callback receives an image stream and must return HOCR XML.
                // This stub returns an empty string; replace with real OCR logic as needed.
                doc.Convert(
                    callback: (imageStream) => string.Empty,
                    flattenImages: true);

                // Save the document as PowerPoint (PPTX)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                doc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

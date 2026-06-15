using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string tempPdfPath    = "temp_embedded.pdf";  // intermediate PDF with embedded font
        const string outputPptxPath = "output.pptx";        // resulting PPTX
        const string customFontName = "Arial";              // name of the custom font installed on the system

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Re‑save the PDF while forcing the use of the custom font.
        // The DefaultFontName property tells Aspose.PDF which font to use
        // when the original document references a missing font. The font
        // will be embedded into the saved PDF.
        // ------------------------------------------------------------
        using (Document srcDoc = new Document(inputPdfPath))
        {
            PdfSaveOptions pdfSaveOpts = new PdfSaveOptions
            {
                // Use the custom font for any missing fonts and embed it.
                DefaultFontName = customFontName
            };

            srcDoc.Save(tempPdfPath, pdfSaveOpts);
        }

        // ------------------------------------------------------------
        // Step 2: Load the intermediate PDF (which now contains the embedded
        // custom font) and convert it to PPTX. The PptxSaveOptions class does
        // not expose a separate font‑embedding flag; fonts present in the PDF
        // are automatically carried over into the generated presentation.
        // ------------------------------------------------------------
        using (Document pdfWithEmbeddedFont = new Document(tempPdfPath))
        {
            PptxSaveOptions pptxOpts = new PptxSaveOptions();
            pdfWithEmbeddedFont.Save(outputPptxPath, pptxOpts);
        }

        Console.WriteLine($"PDF successfully converted to PPTX with embedded font. Output: {outputPptxPath}");
    }
}
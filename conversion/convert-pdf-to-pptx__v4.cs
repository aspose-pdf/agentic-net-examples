using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for drawing‑related classes if needed

class PdfToPptxConverter
{
    static void Main()
    {
        // Paths for the source PDF and the target PPTX file
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";

        // Verify that the input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Convert PDF to PPTX using Aspose.Pdf only
        // -------------------------------------------------
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Aspose.Pdf can directly save a PDF as a PPTX file – no Aspose.Slides required.
            pdfDocument.Save(pptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");

        // -------------------------------------------------
        // Step 2: (Optional) Extract images from the generated PPTX
        // -------------------------------------------------
        // Image extraction from PPTX normally uses the Aspose.Slides API.
        // Because the current project does not reference Aspose.Slides (and the
        // namespace is unavailable), this step is omitted. To enable it, add a
        // reference to the Aspose.Slides assembly and implement the extraction
        // logic similar to the original example.
    }
}

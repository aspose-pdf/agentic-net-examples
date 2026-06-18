using System;
using System.IO;
using Aspose.Pdf; // Core PDF API

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";

        // Validate input PDF
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------
        // 1. Convert PDF to PPTX using Aspose.Pdf core API
        // -------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Save the PDF directly as PPTX – no extra SaveOptions class needed
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // -------------------------------------------------
        // 2. Verify conversion result
        // -------------------------------------------------
        if (!File.Exists(pptxPath))
        {
            Console.Error.WriteLine($"Failed to create PPTX file: {pptxPath}");
            return;
        }

        Console.WriteLine("PDF to PPTX conversion completed successfully.");
    }
}

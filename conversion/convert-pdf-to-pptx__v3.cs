using System;
using System.IO;
using Aspose.Pdf; // PDF handling

class Program
{
    static void Main()
    {
        // Input PDF and output PPTX paths
        const string pdfPath  = "input.pdf";
        const string pptxPath = "output.pptx";

        // Validate the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        // Use a using block to ensure proper disposal of the Document object
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Aspose.Pdf can directly save a PDF as a PPTX file – no Aspose.Slides required
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
    }
}

using System;
using System.IO;
using Aspose.Pdf; // Core PDF API

class Program
{
    static void Main()
    {
        // Paths – adjust as necessary
        const string pdfPath = "input.pdf";
        const string outputPptx = "output.pptx";

        // Verify input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF and directly save it as PPTX using Aspose.Pdf only.
        // Aspose.Pdf supports PDF‑to‑PPTX conversion via the SaveFormat enum.
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(outputPptx, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion complete. Output saved to '{outputPptx}'.");
    }
}

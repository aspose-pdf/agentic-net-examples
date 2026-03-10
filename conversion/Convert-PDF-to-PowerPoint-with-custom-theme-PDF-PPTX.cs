using System;
using System.IO;
using Aspose.Pdf;               // Document, PptxSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";
        const string pptxPath = "output.pptx";

        // Verify source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize PPTX save options (required for non‑PDF output)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // ----- Custom theme placeholder -----
            // Aspose.Pdf does not expose a direct API for applying a PowerPoint theme.
            // If theme‑related settings become available in future versions,
            // they can be configured on the pptxOptions instance here.
            // Example (hypothetical):
            // pptxOptions.Theme = "MyCustomTheme";

            // Save the PDF as a PPTX file using the explicit save options
            pdfDocument.Save(pptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
    }
}
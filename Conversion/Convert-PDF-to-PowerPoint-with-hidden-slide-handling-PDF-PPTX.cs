using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and PptxSaveOptions are in this namespace

class PdfToPptxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output PPTX file path
        const string pptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize save options for PPTX conversion
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // OPTIONAL: control how each PDF page is rendered.
                // Setting SlidesAsImages = false keeps text as editable objects;
                // setting it to true would render each slide as a single image.
                pptxOptions.SlidesAsImages = false;

                // OPTIONAL: if the source PDF contains pages that should not appear
                // as slides (e.g., hidden or auxiliary pages), they can be removed
                // before conversion. Aspose.Pdf does not expose a direct "hidden"
                // flag, so we demonstrate a generic approach: remove pages whose
                // /UserUnit is zero (as an example of a custom filter).
                // Adjust the condition as needed for your specific PDFs.
                for (int i = pdfDocument.Pages.Count; i >= 1; i--)
                {
                    // Example filter – replace with real hidden‑page detection logic
                    if (pdfDocument.Pages[i].UserUnit == 0)
                    {
                        pdfDocument.Pages.Delete(i);
                    }
                }

                // Save the PDF as a PPTX file using the configured options
                pdfDocument.Save(pptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{pptxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
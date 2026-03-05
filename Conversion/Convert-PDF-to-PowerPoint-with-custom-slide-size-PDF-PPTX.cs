using System;
using System.IO;
using Aspose.Pdf; // Core PDF API (includes PptxSaveOptions)

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
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize PPTX save options.
                // No explicit slide‑size property exists in PptxSaveOptions; the generated slides
                // will inherit the size of the source PDF pages. To obtain a custom slide size,
                // adjust the PDF page dimensions before conversion (e.g., by adding a new page
                // with the desired width/height or by scaling existing pages). This example
                // performs a straightforward conversion.
                PptxSaveOptions saveOptions = new PptxSaveOptions();

                // Example of a commonly used option – render each PDF page as an image.
                // Set to false to keep text as editable objects when possible.
                saveOptions.SlidesAsImages = false;

                // Save the PDF as a PPTX file using the specified options.
                pdfDocument.Save(pptxPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf; // Core PDF API and PptxSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output PowerPoint file path
        const string pptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Initialize save options for PPTX conversion (lifecycle rule: explicit SaveOptions)
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    // OPTIONAL: customize conversion behavior.
                    // Example: keep each PDF page as a separate slide (default) and preserve text boxes.
                    OptimizeTextBoxes = true,   // improves text column recognition
                    SlidesAsImages = false      // keep vector/text content when possible
                };

                // Save the PDF as a PowerPoint presentation using the custom options
                pdfDoc.Save(pptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{pptxPath}' created.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Facades;        // Facades namespace (included as requested)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                // Set to false to keep editable shapes (true would render each slide as an image)
                SlidesAsImages = false
                // Additional customizations (e.g., image resolution) can be set here if needed
            };

            // Save the PDF as a PowerPoint presentation
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputPptx}'");
    }
}
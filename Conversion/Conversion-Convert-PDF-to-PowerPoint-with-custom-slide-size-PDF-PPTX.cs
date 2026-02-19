using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (modify as needed)
        const string inputPdfPath = "input.pdf";
        // Output PPTX file path (the .pptx extension tells Aspose.Pdf to save as PowerPoint)
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Define custom slide size (width and height in points)
            // 1 point = 1/72 inch. Adjust values as required.
            const float slideWidth = 960f;   // e.g., 13.33 inches
            const float slideHeight = 540f;  // e.g., 7.5 inches

            // Apply the custom size to every page (each page becomes a slide)
            foreach (Page page in pdfDocument.Pages)
            {
                page.PageInfo.Width = slideWidth;
                page.PageInfo.Height = slideHeight;
            }

            // Save the document as PPTX. The format is inferred from the .pptx extension.
            pdfDocument.Save(outputPptxPath);

            Console.WriteLine($"Conversion successful. PPTX saved to '{outputPptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
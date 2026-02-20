using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ResizePdfExample
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Create a PdfPageEditor facade to modify page properties
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the existing PDF document
                pageEditor.BindPdf(inputPdfPath);

                // Set the desired page size (e.g., A4). 
                // PageSize is an enum defined in Aspose.Pdf namespace.
                pageEditor.PageSize = PageSize.A4;

                // Optionally, adjust zoom if needed (1.0 = 100%)
                pageEditor.Zoom = 1.0f;

                // Save the resized PDF to the output file
                pageEditor.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF resized successfully and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while resizing the PDF: {ex.Message}");
        }
    }
}
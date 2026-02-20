using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output_resized.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Create a PdfPageEditor facade to modify page properties
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the existing PDF document
                pageEditor.BindPdf(inputPdfPath);

                // Set a new page size (e.g., A5). Width and Height are expressed in points (1 point = 1/72 inch)
                pageEditor.PageSize = new PageSize(420, 595);

                // Optionally, you can set a zoom factor (1.0 = 100%)
                // pageEditor.Zoom = 0.8; // Uncomment to apply zoom

                // Save the resized PDF to the specified output path
                pageEditor.Save(outputPdfPath);
            }

            Console.WriteLine($"Resized PDF saved successfully to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while resizing the PDF: {ex.Message}");
        }
    }
}

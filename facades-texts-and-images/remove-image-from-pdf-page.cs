using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the image to be removed
        const string inputPdf  = "input.pdf";
        // Output PDF after the image has been removed
        const string outputPdf = "output.pdf";
        // Object ID (index) of the image to delete on page 4
        const int imageObjectId = 5; // adjust to the actual image index

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (Aspose.Pdf.Facades) to manipulate the PDF
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document
        editor.BindPdf(inputPdf);

        // Delete the specified image from page 4.
        // Page numbers are 1‑based; the image index array contains the object IDs to remove.
        editor.DeleteImage(4, new int[] { imageObjectId });

        // Save the modified PDF
        editor.Save(outputPdf);

        Console.WriteLine($"Image with object ID {imageObjectId} removed from page 4.");
        Console.WriteLine($"Result saved to '{outputPdf}'.");
    }
}
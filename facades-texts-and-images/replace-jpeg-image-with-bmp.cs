using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the JPEG image to replace
        const string inputPdfPath  = "input.pdf";
        // Output PDF after replacement
        const string outputPdfPath = "output.pdf";
        // Higher‑resolution BMP image to use as replacement
        const string bmpImagePath  = "highres.bmp";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bmpImagePath))
        {
            Console.Error.WriteLine($"BMP image not found: {bmpImagePath}");
            return;
        }

        // Bind the PDF to the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);

        // Replace the first image (index = 1) on page 1 with the BMP file
        // Page numbers and image indexes are 1‑based in Aspose.Pdf
        editor.ReplaceImage(pageNumber: 1, index: 1, imageFile: bmpImagePath);

        // Save the modified document
        editor.Save(outputPdfPath);
        // Close the editor (releases internal resources)
        editor.Close();

        Console.WriteLine($"Image replaced successfully. Output saved to '{outputPdfPath}'.");
    }
}
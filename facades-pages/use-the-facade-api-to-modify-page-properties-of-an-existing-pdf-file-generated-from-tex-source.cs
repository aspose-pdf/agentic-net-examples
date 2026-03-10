using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source TeX file, an intermediate PDF, and the final output PDF
        const string texPath = "input.tex";
        const string intermediatePdfPath = "intermediate.pdf";
        const string outputPdfPath = "modified.pdf";

        // Verify the TeX source exists
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX source not found: {texPath}");
            return;
        }

        // --------------------------------------------------------------------
        // 1. Load the TeX file and convert it to a PDF document.
        //    TeXLoadOptions is the appropriate load options class for TeX files.
        // --------------------------------------------------------------------
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        using (Document texDoc = new Document(texPath, texLoadOptions))
        {
            // Save the generated PDF to a temporary file.
            // Document.Save(string) always writes PDF regardless of extension.
            texDoc.Save(intermediatePdfPath);
        }

        // --------------------------------------------------------------------
        // 2. Use the PdfPageEditor facade to modify page properties.
        // --------------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the intermediate PDF file to the facade.
            pageEditor.BindPdf(intermediatePdfPath);

            // Example modifications:
            // Rotate all pages by 90 degrees.
            pageEditor.Rotation = 90; // Valid values: 0, 90, 180, 270

            // Set a zoom factor (1.0 = 100%). Here we use 150%.
            pageEditor.Zoom = 1.5f; // <-- float literal required

            // Move the content origin: shift right by 50 points and up by 30 points.
            pageEditor.MovePosition(50, 30);

            // Change the output page size to A4.
            pageEditor.PageSize = PageSize.A4;

            // Apply the accumulated changes to the document.
            pageEditor.ApplyChanges();

            // Save the modified PDF to the final destination.
            pageEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
    }
}

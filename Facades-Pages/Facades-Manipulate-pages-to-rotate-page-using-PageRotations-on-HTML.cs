using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, intermediate rotated PDF, and final HTML output paths
        const string inputPdfPath = "input.pdf";
        const string rotatedPdfPath = "rotated.pdf";
        const string outputHtmlPath = "output.html";

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Rotate pages using PdfPageEditor (Facades API)
        // ------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the source PDF to the editor
            pageEditor.BindPdf(inputPdfPath);

            // Define page rotations: key = page number (1‑based), value = rotation in degrees
            var rotations = new Dictionary<int, int>
            {
                { 1, 90 },   // Rotate first page 90°
                // Add more entries as needed, e.g. { 2, 180 }
            };
            pageEditor.PageRotations = rotations;

            // Apply the rotation changes
            pageEditor.ApplyChanges();

            // Save the rotated PDF to a temporary file
            pageEditor.Save(rotatedPdfPath);
        }

        // ------------------------------------------------------------
        // 2. Convert the rotated PDF to HTML
        // ------------------------------------------------------------
        using (Document doc = new Document(rotatedPdfPath))
        {
            // HTML conversion requires explicit HtmlSaveOptions
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Save as HTML (extension determines output format)
            doc.Save(outputHtmlPath, htmlOptions);
        }

        Console.WriteLine($"Rotated PDF saved to '{rotatedPdfPath}'.");
        Console.WriteLine($"HTML version saved to '{outputHtmlPath}'.");
    }
}
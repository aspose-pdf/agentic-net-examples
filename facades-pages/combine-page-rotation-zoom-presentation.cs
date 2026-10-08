using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "presentation.pdf";
        const string outputPath = "presentation_transformed.pdf";
        const float zoomFactor = 1.5f; // 150% zoom

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load source PDF to obtain page count (ensuring disposal)
        int pageCount;
        using (Document srcDoc = new Document(inputPath))
        {
            pageCount = srcDoc.Pages.Count;
        }

        // Bind PDF to the editor (PdfPageEditor does not implement IDisposable)
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Apply a 90° clockwise rotation to every page via the PageRotations dictionary
        var rotations = new Dictionary<int, int>();
        for (int i = 1; i <= pageCount; i++)
        {
            rotations[i] = 90; // 90 degrees clockwise
        }
        editor.PageRotations = rotations;

        // Apply zoom to all pages. Use ProcessPages to specify the pages the zoom applies to.
        editor.ProcessPages = Enumerable.Range(1, pageCount).ToArray();
        editor.Zoom = zoomFactor;

        // Save the transformed PDF
        editor.Save(outputPath);
        Console.WriteLine($"Transformed PDF saved to '{outputPath}'.");
    }
}

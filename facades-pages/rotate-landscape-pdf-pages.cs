using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Collect per‑page rotation angles. The PdfPageEditor.PageRotations property
        // expects a Dictionary<int,int> where the key is the 1‑based page number and
        // the value is the rotation angle in degrees.
        var pageRotations = new Dictionary<int, int>();

        // First pass – detect landscape pages.
        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                // Landscape is defined as width greater than height.
                if (page.Rect.Width > page.Rect.Height)
                {
                    // Rotate landscape pages by 90° to make them portrait.
                    pageRotations[i] = 90;
                }
            }
        }

        // Apply the rotations using the PdfPageEditor (facade API).
        var editor = new PdfPageEditor();
        editor.BindPdf(inputPath);
        editor.PageRotations = pageRotations; // Dictionary<int,int> required
        editor.ApplyChanges();
        editor.Save(outputPath);
        editor.Close(); // PdfPageEditor does not implement IDisposable

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}

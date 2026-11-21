using System;
using System.IO;
using System.Reflection;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Convert 10 mm to points (1 pt = 1/72 in, 1 in = 25.4 mm)
        const float mmToPoints = 72f / 25.4f;
        float leftMargin = 10f * mmToPoints; // ≈28.346 pt

        // Try to obtain the PdfStampEditor type via reflection (covers environments where the type is missing at compile‑time)
        Type stampEditorType = Type.GetType("Aspose.Pdf.Facades.PdfStampEditor, Aspose.Pdf");
        if (stampEditorType == null)
        {
            Console.Error.WriteLine("PdfStampEditor type not found in the referenced Aspose.Pdf assembly. Ensure the Aspose.Pdf.Facades package is referenced.");
            return;
        }

        // Create an instance of PdfStampEditor dynamically
        object stampEditorInstance = Activator.CreateInstance(stampEditorType);
        dynamic stampEditor = stampEditorInstance; // use dynamic to call members at runtime

        // Load the PDF
        stampEditor.BindPdf(inputPath);

        int pageNumber = 4;   // target page (1‑based)
        int stampIndex = 1;   // first stamp on the page (1‑based)

        // Retrieve current stamp geometry
        var info = stampEditor.GetStampInfo(pageNumber, stampIndex);
        // StampInfo exposes Left, Bottom, Right, Top properties
        float width  = info.Right - info.Left;
        float height = info.Top   - info.Bottom;

        // Compute new coordinates: left margin = 10 mm, keep vertical position unchanged
        float newLeft   = leftMargin;
        float newBottom = info.Bottom;
        float newRight  = newLeft + width;
        float newTop    = newBottom + height;

        // Apply the new position
        stampEditor.EditStamp(pageNumber, stampIndex, newLeft, newBottom, newRight, newTop);

        // Save the modified PDF
        stampEditor.Save(outputPath);

        Console.WriteLine($"Stamp repositioned on page {pageNumber} and saved to '{outputPath}'.");
    }
}

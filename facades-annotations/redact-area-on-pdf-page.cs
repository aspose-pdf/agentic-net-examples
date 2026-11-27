using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "redacted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the annotation editor facade (does NOT implement IDisposable)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF document to the editor
        editor.BindPdf(inputPath);

        // Define the rectangle to redact (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Redact the specified area on page 5 using LightGray color (System.Drawing.Color)
        editor.RedactArea(5, rect, System.Drawing.Color.LightGray);

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Redacted PDF saved to '{outputPath}'.");
    }
}

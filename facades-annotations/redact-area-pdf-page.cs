using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "redacted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the annotation editor and bind the PDF file
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Define the rectangle to redact (lower‑left X,Y and upper‑right X,Y in points)
            Aspose.Pdf.Rectangle redactRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Redact the area on page 1 using the default redaction color (black)
            // PdfAnnotationEditor.RedactArea expects System.Drawing.Color, so use the fully‑qualified type.
            editor.RedactArea(1, redactRect, System.Drawing.Color.Black);

            // Save the resulting PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Redacted PDF saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfContentEditor and ViewerPreference are defined here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use a using‑statement to guarantee proper disposal of resources
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath); // Load PDF
            // Fully‑qualified enum value from the correct namespace (Aspose.Pdf.Facades)
            editor.ChangeViewerPreference(Aspose.Pdf.Facades.ViewerPreference.PageModeUseNone);
            editor.Save(outputPath); // Persist changes
        }

        Console.WriteLine($"Viewer preference set to UseNone and saved to '{outputPath}'.");
    }
}

/*
 * NOTE: If your project also references System.Drawing (or you need to work with
 * Aspose.Pdf.Drawing types), always use fully‑qualified names for ambiguous
 * types such as Rectangle, Color, Path, etc. Example:
 *
 *   // Rectangle used for page/annotation coordinates
 *   Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
 *
 *   // Color used for PDF objects
 *   Aspose.Pdf.Color pdfColor = Aspose.Pdf.Color.Blue;
 *
 *   // If you need a System.Drawing.Color, qualify it explicitly
 *   System.Drawing.Color sysColor = System.Drawing.Color.Blue;
 *
 *   // Path for file‑system operations
 *   string fileName = System.IO.Path.GetFileName(inputPath);
 *
 * This pattern eliminates CS0104 ambiguous reference errors.
 */
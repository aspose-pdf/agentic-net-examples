using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "output033.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so we instantiate it directly
        Aspose.Pdf.Facades.FormEditor editor = new Aspose.Pdf.Facades.FormEditor();

        // Bind the source PDF document
        editor.BindPdf(inputPath);

        // Move the existing field "CustomerName" to page 2 at position (120,220)
        // MoveField requires the lower‑left (llx,lly) and upper‑right (urx,ury) coordinates.
        // Here we define a rectangle 100 units wide and 20 units high.
        float llx = 120f;   // lower‑left X
        float lly = 220f;   // lower‑left Y
        float urx = 220f;   // upper‑right X (llx + 100)
        float ury = 240f;   // upper‑right Y (lly + 20)

        editor.MoveField("CustomerName", llx, lly, urx, ury);

        // Save the modified PDF to the desired output file
        editor.Save(outputPath);
    }
}
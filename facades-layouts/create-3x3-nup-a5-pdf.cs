using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_3x3_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare the source files array – MakeNUp works with an array of PDFs.
        string[] sourceFiles = new[] { inputPath };

        // Instantiate the editor (PdfFileEditor does not implement IDisposable).
        PdfFileEditor editor = new PdfFileEditor();

        // ---------------------------------------------------------------------
        // Attempt to use NUpOptions via reflection. This keeps the code
        // compilable on older Aspose.Pdf versions where NUpOptions does not exist.
        // If the type and the overload are present, we configure a 3×3 layout
        // on an A5 page. Otherwise we fall back to the simple overload which
        // creates an N‑up PDF with default settings.
        // ---------------------------------------------------------------------
        var nupOptionsType = Type.GetType("Aspose.Pdf.Facades.NUpOptions, Aspose.Pdf");
        if (nupOptionsType != null)
        {
            // Create an instance of NUpOptions dynamically.
            dynamic options = Activator.CreateInstance(nupOptionsType);
            options.Columns = 3;
            options.Rows = 3;
            options.PageSize = Aspose.Pdf.PageSize.A5;

            // Look for the overload: MakeNUp(string[] sourceFiles, string outputPath, bool isLandscape, NUpOptions options)
            var makeNUpMethod = typeof(PdfFileEditor).GetMethod(
                "MakeNUp",
                new[] { typeof(string[]), typeof(string), typeof(bool), nupOptionsType }
            );

            if (makeNUpMethod != null)
            {
                // Landscape flag – false because A5 portrait is desired.
                bool isLandscape = false;
                makeNUpMethod.Invoke(editor, new object[] { sourceFiles, outputPath, isLandscape, options });
                Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
                return;
            }
        }

        // ---------------------------------------------------------------------
        // Fallback: use the basic MakeNUp overload (no explicit layout control).
        // This ensures the program always compiles and runs, even on very old
        // library versions.
        // ---------------------------------------------------------------------
        bool fallbackLandscape = false; // portrait orientation
        editor.MakeNUp(sourceFiles, outputPath, fallbackLandscape);
        Console.WriteLine($"N‑up PDF saved to '{outputPath}' (default layout).");
    }
}

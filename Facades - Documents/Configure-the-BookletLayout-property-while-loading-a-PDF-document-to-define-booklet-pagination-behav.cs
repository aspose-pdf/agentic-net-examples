using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // NOTE: In older Aspose.Pdf versions the BindPdf method and the BookletLayout
        // property (or SetBookletLayout method) are not available. The MakeBooklet
        // method internally loads the source PDF, therefore explicit binding and
        // layout configuration are unnecessary. If you need to control booklet
        // pagination (e.g., Folded, Unfolded, SaddleStitch, etc.), upgrade to a newer
        // Aspose.Pdf package that provides the BookletLayout enum and related API.

        bool created = editor.MakeBooklet(inputPath, outputPath);

        if (created)
            Console.WriteLine($"Booklet created successfully at '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create booklet.");
    }
}

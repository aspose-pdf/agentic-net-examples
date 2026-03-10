using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet_output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so no using block is needed.
        PdfFileEditor editor = new PdfFileEditor();

        // NOTE: The version of Aspose.Pdf referenced by this project does not provide
        // the BindPdf method or the BookletLayout property. The MakeBooklet method can
        // operate directly with the input and output file paths, so we call it
        // without any additional configuration. If you need to control booklet
        // pagination (e.g., LeftRight, RightLeft, SaddleStitch, etc.), upgrade to a
        // newer Aspose.Pdf release that includes the SetBookletLayout method and the
        // BookletLayout enum.
        bool created = editor.MakeBooklet(inputPath, outputPath);

        if (created)
            Console.WriteLine($"Booklet created successfully: {outputPath}");
        else
            Console.Error.WriteLine("Failed to create booklet.");
    }
}

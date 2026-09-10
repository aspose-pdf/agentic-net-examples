using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Create a minimal input PDF if it does not exist.
        if (!System.IO.File.Exists(inputPath))
        {
            using var seed = new Document();
            seed.Pages.Add();
            seed.Save(inputPath);
        }

        // Uniform margin of 10% on all sides.
        var parameters = PdfFileEditor.ContentsResizeParameters.MarginsPercent(
            left: 10,
            right: 10,
            top: 10,
            bottom: 10);

        // PdfFileEditor does not implement IDisposable.
        var editor = new PdfFileEditor();
        bool success = editor.ResizeContents(inputPath, outputPath, null, parameters);

        if (!success)
            Console.Error.WriteLine("Resize operation failed.");
        else
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}

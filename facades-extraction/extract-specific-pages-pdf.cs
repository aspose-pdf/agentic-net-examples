using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file (replace with your actual source)
        const string sourcePath = "source.pdf";
        // Path for the resulting PDF containing the selected pages
        const string resultPath = "selected_stream.pdf";

        // Ensure the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Extract pages 25, 30 and 35 from the source file into the result file.
        // The correct overload order is: source file path, int[] page numbers, output file path.
        editor.Extract(sourcePath, new int[] { 25, 30, 35 }, resultPath);

        Console.WriteLine($"Pages 25, 30, 35 extracted to '{resultPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string sourcePdfPath = "source.pdf";
        // Path for the extracted page range
        const string outputPdfPath = "stream_range5_10.pdf";

        // Verify the source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Use the stream‑based overload of PdfFileEditor.Extract
        using (FileStream input = File.OpenRead(sourcePdfPath))
        using (FileStream output = File.Create(outputPdfPath))
        {
            PdfFileEditor editor = new PdfFileEditor();
            // Correct overload: (Stream input, int startPage, int endPage, Stream output)
            editor.Extract(input, 5, 10, output);
        }

        Console.WriteLine($"Pages 5‑10 have been extracted to '{outputPdfPath}'.");
    }
}

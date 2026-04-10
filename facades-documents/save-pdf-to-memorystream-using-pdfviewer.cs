using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (var outputStream = new MemoryStream())
        {
            using (var viewer = new PdfViewer())
            {
                viewer.BindPdf(inputPath);
                // PdfViewer does not expose a Zoom property in the current Aspose.Pdf version.
                // If a zoom effect is required, use PdfPageEditor before binding, or render to images.
                viewer.Save(outputStream);
            }

            outputStream.Position = 0;
            Console.WriteLine($"PDF saved to memory stream, length = {outputStream.Length} bytes");
            // The MemoryStream now contains the PDF.
        }
    }
}

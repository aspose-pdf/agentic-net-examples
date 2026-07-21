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

        // MemoryStream will receive the modified PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Facade for page-level editing
            PdfPageEditor editor = new PdfPageEditor();

            // Load the source PDF into the facade
            editor.BindPdf(inputPath);

            // Example modification: change zoom factor
            editor.Zoom = 0.75f;

            // Save the result directly into the MemoryStream
            editor.Save(outputStream);

            // Prepare the stream for subsequent reading/processing
            outputStream.Position = 0;

            Console.WriteLine($"Modified PDF saved to memory stream (size: {outputStream.Length} bytes).");
        }
    }
}
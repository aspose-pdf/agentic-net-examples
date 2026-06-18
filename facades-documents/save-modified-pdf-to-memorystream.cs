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

        // MemoryStream that will receive the modified PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Facade for page-level editing
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the source PDF into the facade
                editor.BindPdf(inputPath);

                // Example modification: change the zoom factor
                editor.Zoom = 0.5f;

                // Save the result directly into the MemoryStream
                editor.Save(outputStream);
            }

            // Reset the stream position if it will be read later
            outputStream.Position = 0;

            // Demonstrate that the stream contains data
            Console.WriteLine($"Modified PDF saved to MemoryStream, length = {outputStream.Length} bytes");
        }
    }
}
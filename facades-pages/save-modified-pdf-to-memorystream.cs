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

        using (MemoryStream outputStream = new MemoryStream())
        {
            // Load the PDF with a facade
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(inputPath);

            // Example modification (optional)
            editor.Zoom = 0.75f;

            // Save the modified PDF into the memory stream
            editor.Save(outputStream);

            // Prepare the stream for further use
            outputStream.Position = 0;

            Console.WriteLine($"Modified PDF size in memory: {outputStream.Length} bytes");

            // Release resources held by the facade
            editor.Close();
        }
    }
}
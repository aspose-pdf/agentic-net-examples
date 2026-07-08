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

        // MemoryStream will hold the modified PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // PdfPageEditor is a saveable facade from Aspose.Pdf.Facades
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the source PDF
                editor.BindPdf(inputPath);

                // Example modification – change zoom factor
                editor.Zoom = 0.75f;

                // Save the result directly into the MemoryStream
                editor.Save(outputStream);
            }

            // Reset position for any subsequent processing
            outputStream.Position = 0;

            // Example: display the size of the in‑memory PDF
            Console.WriteLine($"Modified PDF size in memory: {outputStream.Length} bytes");
            // Further processing can be performed using 'outputStream' without touching the file system
        }
    }
}
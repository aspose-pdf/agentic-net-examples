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

        // Bind the PDF file to the facade for editing
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Example modification: adjust zoom (any other edit can be applied here)
            editor.Zoom = 0.8f;

            // Save the modified PDF into a MemoryStream (no disk I/O)
            using (MemoryStream memory = new MemoryStream())
            {
                editor.Save(memory);
                memory.Position = 0; // Reset for downstream processing

                // The PDF is now available in 'memory' for further use
                Console.WriteLine($"Modified PDF stored in memory: {memory.Length} bytes");
            }
        }
    }
}
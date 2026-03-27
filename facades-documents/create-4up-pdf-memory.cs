using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the input PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Prepare an in‑memory stream for the output PDF
            using (MemoryStream outputStream = new MemoryStream())
            {
                // PdfFileEditor does not implement IDisposable, so we instantiate it directly
                PdfFileEditor editor = new PdfFileEditor();

                // Apply a 4‑up layout (2 columns × 2 rows)
                bool success = editor.MakeNUp(inputStream, outputStream, 2, 2);

                if (!success)
                {
                    Console.Error.WriteLine("MakeNUp operation failed.");
                    return;
                }

                // Reset the position of the memory stream if further processing is needed
                outputStream.Position = 0;

                // Example: write the memory stream to a physical file for verification
                using (FileStream fileOut = new FileStream("output.pdf", FileMode.Create, FileAccess.Write))
                {
                    outputStream.CopyTo(fileOut);
                }

                Console.WriteLine("4‑up PDF created in memory and saved to output.pdf.");
            }
        }
    }
}

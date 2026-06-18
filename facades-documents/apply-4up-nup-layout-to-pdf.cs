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

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Prepare an in‑memory stream for the result
            using (MemoryStream outputStream = new MemoryStream())
            {
                // PdfFileEditor provides the N‑Up operation via stream overloads
                PdfFileEditor editor = new PdfFileEditor();

                // 4‑up grid = 2 columns × 2 rows
                bool success = editor.MakeNUp(inputStream, outputStream, 2, 2);

                if (!success)
                {
                    Console.Error.WriteLine("MakeNUp operation failed.");
                    return;
                }

                // Reset the position so the caller can read from the beginning
                outputStream.Position = 0;

                // At this point outputStream contains the N‑Up PDF in memory
                Console.WriteLine("4‑up grid applied successfully; output is in memory.");
            }
        }
    }
}
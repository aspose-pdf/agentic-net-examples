using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "split.pdf";
        const int endPage = 5; // split up to this page (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF into a memory stream (in‑memory processing)
        using (FileStream fileInput = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream inputStream = new MemoryStream())
        {
            fileInput.CopyTo(inputStream);
            inputStream.Position = 0;

            using (MemoryStream outputStream = new MemoryStream())
            {
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.SplitFromFirst(inputStream, endPage, outputStream);
                if (!success)
                {
                    Console.Error.WriteLine("PDF split operation failed.");
                    return;
                }

                // Write the resulting split PDF to a file (simple filename as required)
                outputStream.Position = 0;
                using (FileStream fileOutput = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    outputStream.CopyTo(fileOutput);
                }
                Console.WriteLine($"Split PDF saved to '{outputPath}'.");
            }
        }
    }
}
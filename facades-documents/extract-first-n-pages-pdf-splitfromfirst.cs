using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF file
        const int endPage = 5;                  // split up to this page (inclusive)
        const string outputPath = "front_part.pdf"; // where to write the result (optional)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        // Prepare an in‑memory stream for the split result
        using (MemoryStream frontPartStream = new MemoryStream())
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap it in a using block
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the first page up to 'endPage' and write the front part into the memory stream
            bool success = editor.SplitFromFirst(sourceStream, endPage, frontPartStream);

            if (!success)
            {
                Console.Error.WriteLine("Split operation failed.");
                return;
            }

            // Reset the position so the stream can be read from the beginning
            frontPartStream.Position = 0;

            // Optionally, save the in‑memory result to a physical file
            using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                frontPartStream.CopyTo(fileOut);
            }

            Console.WriteLine($"Pages 1‑{endPage} extracted to '{outputPath}'.");
        }
    }
}
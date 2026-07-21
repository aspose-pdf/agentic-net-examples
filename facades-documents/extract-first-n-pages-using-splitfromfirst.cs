using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int endPage = 5; // split up to this page (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Input and output streams are disposed automatically via using
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream outputStream = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the first page to the specified end page, result goes to outputStream
            bool success = editor.SplitFromFirst(inputStream, endPage, outputStream);
            if (!success)
            {
                Console.Error.WriteLine("SplitFromFirst operation failed.");
                return;
            }

            // Prepare the output stream for reading
            outputStream.Position = 0;

            // Example: write the in‑memory result to a physical file
            const string outputPath = "split_front_part.pdf";
            using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                outputStream.CopyTo(fileOut);
            }

            Console.WriteLine($"Pages 1‑{endPage} saved to '{outputPath}'.");
        }
    }
}
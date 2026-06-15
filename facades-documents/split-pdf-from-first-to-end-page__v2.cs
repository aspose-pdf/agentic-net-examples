using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF file
        const string outputPath = "output.pdf";  // destination PDF file
        const int endPage       = 5;             // split up to this page (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream.
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        // Prepare an in‑memory stream for the split result.
        using (MemoryStream outputStream = new MemoryStream())
        {
            // PdfFileEditor provides the stream‑based split operation.
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the first page to the specified end page.
            bool success = editor.SplitFromFirst(inputStream, endPage, outputStream);

            if (!success)
            {
                Console.Error.WriteLine("Failed to split the PDF.");
                return;
            }

            // Persist the split PDF to a file (optional step).
            File.WriteAllBytes(outputPath, outputStream.ToArray());
            Console.WriteLine($"Split PDF saved to '{outputPath}'.");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int endPage = 5; // split up to this page (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open input and output streams; they will be disposed automatically.
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the first page to the specified end page and write to the output stream.
            bool result = editor.SplitFromFirst(inputStream, endPage, outputStream);

            Console.WriteLine(result
                ? $"Successfully split pages 1-{endPage} to '{outputPath}'."
                : "Failed to split the PDF.");
        }
    }
}
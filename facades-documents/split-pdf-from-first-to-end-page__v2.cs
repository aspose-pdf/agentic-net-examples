using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int endPage = 5; // inclusive end page for the split

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open input and output streams; they will be disposed automatically
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the first page up to the specified end page
            bool result = editor.SplitFromFirst(inputStream, endPage, outputStream);

            if (result)
                Console.WriteLine($"Successfully split pages 1-{endPage} to '{outputPath}'.");
            else
                Console.Error.WriteLine("Split operation failed.");
        }
    }
}
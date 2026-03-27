using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open input and output streams
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block
            PdfFileEditor editor = new PdfFileEditor();
            // Create booklet with a custom page size (e.g., A5)
            bool succeeded = editor.MakeBooklet(inputStream, outputStream, PageSize.A5);
            Console.WriteLine(succeeded ? "Booklet created successfully." : "Failed to create booklet.");
        }
    }
}
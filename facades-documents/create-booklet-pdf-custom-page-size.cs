using System;
using System.IO;
using Aspose.Pdf;               // PageSize enum
using Aspose.Pdf.Facades;      // PdfFileEditor facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "booklet.pdf";    // booklet output

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open input and output streams inside using blocks for deterministic disposal
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Instantiate the PdfFileEditor facade (does not implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet from the input stream, write to the output stream,
            // and set a custom page size (e.g., A5). PageSize is an enum; choose the size you need.
            bool result = editor.MakeBooklet(inputStream, outputStream, PageSize.A5);

            Console.WriteLine(result ? "Booklet created successfully." : "Failed to create booklet.");
        }
    }
}
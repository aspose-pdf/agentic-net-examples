using System;
using System.IO;
using Aspose.Pdf;               // PageSize enum
using Aspose.Pdf.Facades;      // PdfFileEditor facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the input and output streams; using ensures proper disposal
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Initialize the PdfFileEditor facade
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet with a custom page size (e.g., A5)
            bool result = editor.MakeBooklet(inputStream, outputStream, PageSize.A5);

            Console.WriteLine(result
                ? "Booklet created successfully."
                : "Failed to create booklet.");
        }
    }
}
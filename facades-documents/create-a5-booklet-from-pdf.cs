using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "booklet_A5.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so no using block is needed
        PdfFileEditor editor = new PdfFileEditor();

        // Use the MakeBooklet overload that accepts a source path, destination path, and a PageSize enum.
        // This method returns a bool indicating success.
        bool result = editor.MakeBooklet(inputPath, outputPath, PageSize.A5);

        if (result)
            Console.WriteLine($"Booklet created successfully at '{outputPath}' with A5 page size.");
        else
            Console.Error.WriteLine("Failed to create booklet.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;               // Contains the PageSize enum
using Aspose.Pdf.Facades;      // Contains PdfFileEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Source PDF
        const string outputPath = "booklet.pdf"; // Destination booklet PDF

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source and destination streams inside using blocks for deterministic disposal
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Instantiate the PdfFileEditor facade (it does not implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet from the input stream and write it to the output stream.
            // The third argument sets the page size of the resulting booklet.
            // Here we use PageSize.A5 as an example of a custom dimension.
            bool result = editor.MakeBooklet(inputStream, outputStream, PageSize.A5);

            if (!result)
            {
                Console.Error.WriteLine("Failed to generate booklet.");
                return;
            }
        }

        Console.WriteLine($"Booklet successfully created at '{outputPath}'.");
    }
}
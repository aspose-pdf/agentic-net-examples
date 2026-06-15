using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "booklet.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open input and output streams with deterministic disposal
        using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            // Custom page size: 5.5 x 8.5 inches (1 inch = 72 points)
            PageSize customSize = new PageSize(5.5f * 72f, 8.5f * 72f);

            // Create the facade and generate the booklet
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputStream, outputStream, customSize);

            if (!success)
            {
                Console.Error.WriteLine("Failed to create booklet.");
            }
        }

        Console.WriteLine($"Booklet saved to '{outputPdfPath}'.");
    }
}
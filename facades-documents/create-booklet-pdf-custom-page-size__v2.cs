using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BookletCreator
{
    static void Main()
    {
        // Input PDF stream (replace with your source stream as needed)
        const string inputPdfPath = "input.pdf";
        // Output PDF file path
        const string outputPdfPath = "booklet_output.pdf";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Define custom page size: 5.5 x 8.5 inches (1 inch = 72 points)
        // Width = 5.5 * 72 = 396 points, Height = 8.5 * 72 = 612 points
        PageSize customSize = new PageSize(396f, 612f);

        // Create PdfFileEditor facade
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // Open input and output streams
        using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            // Generate booklet with the specified custom page size
            bool success = pdfEditor.MakeBooklet(inputStream, outputStream, customSize);

            if (!success)
            {
                Console.Error.WriteLine("Failed to create booklet.");
                return;
            }
        }

        Console.WriteLine($"Booklet created successfully at '{outputPdfPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF stream (replace with your actual source)
        const string inputPdfPath  = "input.pdf";
        // Output PDF file path
        const string outputPdfPath = "booklet_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Define custom page size: 5.5 x 8.5 inches (1 inch = 72 points)
        float widthPoints  = 5.5f * 72f; // 396 points
        float heightPoints = 8.5f * 72f; // 612 points
        PageSize customSize = new PageSize(widthPoints, heightPoints);

        // Open input and output streams with proper disposal
        using (FileStream inputStream  = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor provides the MakeBooklet operation for streams
            PdfFileEditor editor = new PdfFileEditor();

            // Create booklet with the custom page size
            bool success = editor.MakeBooklet(inputStream, outputStream, customSize);

            if (success)
                Console.WriteLine($"Booklet created successfully: {outputPdfPath}");
            else
                Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}
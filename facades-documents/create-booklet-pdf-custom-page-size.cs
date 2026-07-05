using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting booklet PDF
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "booklet.pdf";

        // Ensure the input file exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the input PDF as a read‑only stream and the output PDF as a writeable stream
        using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            // Define a custom page size of 5.5 x 8.5 inches.
            // 1 inch = 72 points, so width = 5.5 * 72, height = 8.5 * 72.
            PageSize customPageSize = new PageSize(5.5f * 72f, 8.5f * 72f);

            // Create the PdfFileEditor facade and generate the booklet.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputStream, outputStream, customPageSize);

            if (!success)
            {
                Console.Error.WriteLine("Failed to create booklet.");
                return;
            }
        }

        Console.WriteLine($"Booklet created successfully: {outputPdfPath}");
    }
}
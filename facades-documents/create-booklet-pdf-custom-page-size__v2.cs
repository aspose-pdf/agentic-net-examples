using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // PageSize resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Custom page size: 5.5 x 8.5 inches.
        // Aspose.Pdf uses points (1 inch = 72 points).
        float widthPoints  = 5.5f * 72f; // 396 points
        float heightPoints = 8.5f * 72f; // 612 points
        PageSize customSize = new PageSize(widthPoints, heightPoints);

        // Open input and output streams and create the booklet.
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool result = editor.MakeBooklet(inputStream, outputStream, customSize);
            Console.WriteLine(result ? "Booklet created successfully." : "Failed to create booklet.");
        }
    }
}
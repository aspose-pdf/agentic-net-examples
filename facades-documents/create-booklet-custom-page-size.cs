using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Custom page size: 5.5 inches x 8.5 inches (1 inch = 72 points)
        // PageSize constructor expects float values, so use float literals or cast.
        float widthPoints = 5.5f * 72f;
        float heightPoints = 8.5f * 72f;
        PageSize customSize = new PageSize(widthPoints, heightPoints);

        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputStream, outputStream, customSize);
            if (success)
            {
                Console.WriteLine($"Booklet created successfully: {outputPath}");
            }
            else
            {
                Console.Error.WriteLine("Failed to create booklet.");
            }
        }
    }
}

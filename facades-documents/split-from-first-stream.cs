using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int endPage = 5; // split from first page up to this page (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor pdfEditor = new PdfFileEditor();
            bool success = pdfEditor.SplitFromFirst(inputStream, endPage, outputStream);
            if (success)
            {
                Console.WriteLine($"Pages 1-{endPage} saved to {outputPath}");
            }
            else
            {
                Console.Error.WriteLine("Split operation failed.");
            }
        }
    }
}
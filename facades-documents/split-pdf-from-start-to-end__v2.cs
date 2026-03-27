using System;
using System.IO;
using Aspose.Pdf.Facades;

public class PdfSplitter
{
    public static MemoryStream SplitFromPageToEnd(Stream inputPdfStream, int startPage)
    {
        MemoryStream outputStream = new MemoryStream();
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.SplitToEnd(inputPdfStream, startPage, outputStream);
        if (!result)
        {
            throw new InvalidOperationException("Split operation failed.");
        }
        outputStream.Position = 0;
        return outputStream;
    }
}

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const int startPage = 3;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            MemoryStream resultStream = PdfSplitter.SplitFromPageToEnd(inputStream, startPage);
            // Demonstration: save the resulting stream to a file
            using (FileStream fileOut = new FileStream("output.pdf", FileMode.Create, FileAccess.Write))
            {
                resultStream.CopyTo(fileOut);
            }
            Console.WriteLine("Split PDF saved to output.pdf");
        }
    }
}
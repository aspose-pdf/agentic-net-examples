using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageNumber = 4;
        const int imageObjectId = 2; // replace with the actual image object ID to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfContentEditor contentEditor = new PdfContentEditor();
        contentEditor.BindPdf(inputPath);
        contentEditor.DeleteImage(pageNumber, new int[] { imageObjectId });
        contentEditor.Save(outputPath);
        Console.WriteLine($"Removed image with object ID {imageObjectId} from page {pageNumber} and saved as '{outputPath}'.");
    }
}
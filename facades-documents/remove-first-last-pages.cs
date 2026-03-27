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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        if (pageCount < 2)
        {
            Console.Error.WriteLine("Document does not have enough pages to remove first and last.");
            return;
        }

        int[] pagesToDelete = new int[] { 1, pageCount };

        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Delete(inputPath, pagesToDelete, outputPath);
        if (result)
        {
            Console.WriteLine($"First and last pages removed. Saved as {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete pages.");
        }
    }
}
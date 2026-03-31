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

        using (Document doc = new Document(inputPath))
        {
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Define the page range (e.g., pages 2 through 5)
            int startPage = 2;
            int endPage = 5;
            int rangeLength = endPage - startPage + 1;
            int[] pageRange = new int[rangeLength];
            for (int i = 0; i < rangeLength; i++)
            {
                pageRange[i] = startPage + i;
            }

            editor.ProcessPages = pageRange;   // pages to be edited
            editor.TransitionDuration = 1;    // one second per page
            editor.ApplyChanges();

            doc.Save(outputPath);
        }

        Console.WriteLine($"Transition duration set for pages {2}-{5} and saved to '{outputPath}'.");
    }
}

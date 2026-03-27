using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            Console.WriteLine($"Document has {pageCount} page(s).");

            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];
                int annotationCount = page.Annotations.Count;
                Console.WriteLine($"Page {i}: {annotationCount} annotation(s)");
            }
        }
    }
}
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

        // Determine even‑numbered pages (1‑based indexing)
        int[] evenPages;
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            int evenCount = pageCount / 2;
            evenPages = new int[evenCount];
            int index = 0;
            for (int i = 2; i <= pageCount; i += 2)
            {
                evenPages[index++] = i;
            }
        }

        PdfFileEditor fileEditor = new PdfFileEditor();
        bool success = fileEditor.ResizeContentsPct(inputPath, outputPath, evenPages, 80.0, 80.0);
        if (success)
        {
            Console.WriteLine($"Even pages resized to 80% and saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize pages.");
        }
    }
}
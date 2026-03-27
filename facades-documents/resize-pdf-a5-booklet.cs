using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process (replace with actual file names)
        string[] inputFiles = new string[] { "document1.pdf", "document2.pdf" };

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Create a resized copy with A5 page size
            string resizedFileName = Path.GetFileNameWithoutExtension(inputPath) + "_resized.pdf";
            using (Document doc = new Document(inputPath))
            {
                foreach (Page page in doc.Pages)
                {
                    // Resize each page to A5 – SetPageSize expects width and height as doubles
                    page.SetPageSize((double)PageSize.A5.Width, (double)PageSize.A5.Height);
                }
                doc.Save(resizedFileName);
            }

            // Generate a booklet from the resized PDF
            string bookletFileName = Path.GetFileNameWithoutExtension(inputPath) + "_booklet.pdf";
            PdfFileEditor editor = new PdfFileEditor();
            editor.MakeBooklet(resizedFileName, bookletFileName);

            Console.WriteLine($"Processed '{inputPath}' -> resized: '{resizedFileName}', booklet: '{bookletFileName}'");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Ensure the source PDF exists. If it does not, create a dummy PDF.
        // NOTE: In evaluation mode Aspose.PDF allows a maximum of 4 elements in any collection (Pages, Annotations, etc.).
        // Therefore we create at most 4 pages to avoid the runtime IndexOutOfRangeException.
        if (!File.Exists(inputPath))
        {
            CreateDummyPdf(inputPath, 12); // request 12, but the method will cap at 4.
            Console.WriteLine($"Dummy PDF created at '{inputPath}'.");
        }

        using (Document document = new Document(inputPath))
        {
            // The original task requires copying MediaBox from page 8 to page 12.
            // This can only be performed when the document actually contains those pages.
            // In evaluation mode we cannot have more than 4 pages, so we guard the operation.
            if (document.Pages.Count >= 12)
            {
                Rectangle mediaBox = document.Pages[8].MediaBox;
                document.Pages[12].MediaBox = mediaBox;
                Console.WriteLine("MediaBox from page 8 applied to page 12.");
            }
            else
            {
                Console.WriteLine("Document does not have enough pages to perform the MediaBox copy (requires at least 12 pages)." +
                                  " This limitation is due to the evaluation mode cap of 4 pages.");
            }

            document.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
    }

    /// <summary>
    /// Creates a simple PDF with the specified number of blank pages.
    /// In evaluation mode the page count is capped at 4 to avoid the collection‑size limitation.
    /// </summary>
    /// <param name="path">File path where the PDF will be saved.</param>
    /// <param name="pageCount">Requested number of pages to generate.</param>
    private static void CreateDummyPdf(string path, int pageCount)
    {
        // Cap the number of pages at 4 for evaluation mode.
        int pagesToCreate = Math.Min(pageCount, 4);
        using (Document doc = new Document())
        {
            for (int i = 0; i < pagesToCreate; i++)
            {
                // Add a blank page with default size (A4)
                doc.Pages.Add();
            }
            doc.Save(path);
        }
    }
}

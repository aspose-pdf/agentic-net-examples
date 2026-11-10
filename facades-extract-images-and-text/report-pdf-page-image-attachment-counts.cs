using System;
using System.IO;
using Aspose.Pdf;

class PdfDiagnosticTool
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        int pageCount;
        int attachmentCount;
        int imageCount = 0;

        // Load the document once – it provides page count, embedded files (attachments) and resources.
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            pageCount = doc.Pages.Count;

            // Attachments are represented by embedded files in the PDF.
            attachmentCount = doc.EmbeddedFiles != null ? doc.EmbeddedFiles.Count : 0;

            // Count images on each page.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // page.Resources.Images implements IEnumerable<XImage>
                foreach (XImage img in page.Resources.Images)
                {
                    imageCount++;
                }
            }
        }

        Console.WriteLine($"Pages: {pageCount}");
        Console.WriteLine($"Images: {imageCount}");
        Console.WriteLine($"Attachments: {attachmentCount}");
    }
}

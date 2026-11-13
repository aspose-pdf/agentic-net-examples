using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class PdfSummaryReport
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string pdfFolder = "PdfFiles";

        if (!Directory.Exists(pdfFolder))
        {
            Console.Error.WriteLine($"Folder not found: {pdfFolder}");
            return;
        }

        // Iterate over each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(pdfFolder, "*.pdf"))
        {
            // Ensure the file exists before processing
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Open the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // ---------- Text extraction ----------
                TextAbsorber absorber = new TextAbsorber();
                // Use default extraction options (pure text)
                doc.Pages.Accept(absorber);
                int totalTextLength = absorber.Text?.Length ?? 0;

                // ---------- Image counting ----------
                int imageCount = 0;
                // Pages are 1‑based; iterate accordingly
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    // XImageCollection is enumerable; count each XImage
                    foreach (XImage img in page.Resources.Images)
                    {
                        imageCount++;
                    }
                }

                // ---------- Vector graphics counting ----------
                int graphicsCount = 0;
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    // Page.Contents holds drawing objects; count Path objects (vector graphics)
                    graphicsCount += page.Contents.OfType<Aspose.Pdf.Drawing.Path>().Count();
                }

                // Output the summary for the current PDF
                Console.WriteLine($"File: {System.IO.Path.GetFileName(pdfPath)}");
                Console.WriteLine($"  Extracted graphics (vector paths): {graphicsCount}");
                Console.WriteLine($"  Total text length (characters):   {totalTextLength}");
                Console.WriteLine($"  Image count:                      {imageCount}");
                Console.WriteLine();
            }
        }
    }
}

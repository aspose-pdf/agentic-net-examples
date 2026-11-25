using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Paths to the source PDF files and the output PNG image
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string outputPng = "difference.png";

        // Verify that the input files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load both PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Choose the pages to compare (1‑based indexing)
            int pageNumber1 = 1; // first page of the first document
            int pageNumber2 = 1; // first page of the second document

            // Ensure the requested pages exist
            if (pageNumber1 < 1 || pageNumber1 > doc1.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number for the first PDF.");
                return;
            }
            if (pageNumber2 < 1 || pageNumber2 > doc2.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number for the second PDF.");
                return;
            }

            // Retrieve the actual Page objects (Aspose.Pdf uses 1‑based indexing for the Pages collection)
            Page page1 = doc1.Pages[pageNumber1];
            Page page2 = doc2.Pages[pageNumber2];

            // Configure the graphical comparer – Resolution, Threshold and Color are properties, not method arguments
            var comparer = new GraphicalPdfComparer
            {
                Resolution = new Resolution(150), // 150 DPI – adjust as needed
                Threshold = 0,                    // No tolerance; change if you need fuzzy matching
                Color = Aspose.Pdf.Color.Red      // Highlight colour for differences (fully qualified to avoid ambiguity)
            };

            // Perform the comparison and obtain the image difference
            using (ImagesDifference diff = comparer.GetDifference(page1, page2))
            using (Bitmap destImage = diff.GetDestinationImage()) // System.Drawing.Bitmap
            {
                destImage.Save(outputPng, ImageFormat.Png);
            }

            Console.WriteLine($"Difference image saved to '{outputPng}'.");
        }
    }
}

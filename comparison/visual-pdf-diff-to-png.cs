using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string diffPdfPath = "diff.pdf"; // temporary diff PDF

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Load the two source PDFs inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create the graphical comparer and set the visual similarity threshold.
            var comparer = new GraphicalPdfComparer
            {
                Threshold = 0.1 // 0.0 (identical) to 1.0 (completely different)
            };

            // Generate a PDF that contains the visual differences.
            comparer.CompareDocumentsToPdf(doc1, doc2, diffPdfPath);

            // Load the generated diff PDF.
            using (Document diffDoc = new Document(diffPdfPath))
            {
                if (diffDoc.Pages.Count == 0)
                {
                    Console.WriteLine("No differences detected.");
                    return;
                }

                // Export each diff page as a separate PNG image.
                for (int i = 1; i <= diffDoc.Pages.Count; i++) // 1‑based indexing
                {
                    string pngPath = $"diff_page_{i}.png";
                    using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                    {
                        // PngDevice renders a PDF page to a PNG image.
                        var pngDevice = new PngDevice();
                        pngDevice.Process(diffDoc.Pages[i], pngStream);
                    }
                    Console.WriteLine($"Saved diff image: {pngPath}");
                }
            }
        }
    }
}

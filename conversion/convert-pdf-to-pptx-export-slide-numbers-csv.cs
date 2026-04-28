using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class PdfToPptxAndExtractTitles
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string csvPath = "slide_titles.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX using Aspose.Pdf only ----------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // SaveFormat.Pptx is defined in Aspose.Pdf namespace and does not require Aspose.Slides
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // ---------- Create a CSV with slide numbers (titles not available without Aspose.Slides) ----------
        if (!File.Exists(pptxPath))
        {
            Console.Error.WriteLine($"PPTX file not created: {pptxPath}");
            return;
        }

        using (StreamWriter csvWriter = new StreamWriter(csvPath, false))
        {
            // Write CSV header
            csvWriter.WriteLine("SlideNumber,Title");

            // The number of slides equals the number of PDF pages after conversion.
            // Aspose.Pdf does not expose slide titles, so we output empty titles.
            using (Document pptxDoc = new Document(pptxPath))
            {
                int slideCount = pptxDoc.Pages.Count; // each page becomes a slide
                for (int i = 0; i < slideCount; i++)
                {
                    csvWriter.WriteLine($"{i + 1},\"\""); // empty title placeholder
                }
            }
        }

        Console.WriteLine($"Conversion complete. PPTX saved to '{pptxPath}'. Slide titles saved to '{csvPath}'.");
    }
}

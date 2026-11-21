using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfToPptxAndExtractTitles
{
    static void Main()
    {
        // Input PDF, intermediate PPTX, and output CSV paths
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string csvPath = "slide_titles.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Save directly to PPTX format – no separate SaveOptions class is required
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // ---------- Extract slide titles from the original PDF (as a proxy for PPTX slide titles) ----------
        using (Document pdfDoc = new Document(pdfPath))
        using (StreamWriter csvWriter = new StreamWriter(csvPath, false))
        {
            // Write CSV header
            csvWriter.WriteLine("SlideNumber,Title");

            int slideNumber = 1;
            foreach (Page page in pdfDoc.Pages)
            {
                // Extract all text from the page
                TextAbsorber absorber = new TextAbsorber();
                page.Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // Assume the first non‑empty line is the slide title
                string title = string.Empty;
                using (StringReader sr = new StringReader(pageText))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (!string.IsNullOrEmpty(line))
                        {
                            title = line;
                            break;
                        }
                    }
                }

                // Escape double quotes in title for CSV compliance
                string escapedTitle = title.Replace("\"", "\"\"");
                csvWriter.WriteLine($"{slideNumber},\"{escapedTitle}\"");
                slideNumber++;
            }
        }

        Console.WriteLine($"Conversion complete. PPTX saved to '{pptxPath}'. Slide titles exported to '{csvPath}'.");
    }
}

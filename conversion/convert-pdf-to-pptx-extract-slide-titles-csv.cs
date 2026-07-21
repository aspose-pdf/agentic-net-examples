using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPptxPath = "output.pptx";
        const string outputCsvPath  = "slide_titles.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // ---------- Convert PDF to PPTX ----------
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                pdfDoc.Save(outputPptxPath, pptxOptions);
                Console.WriteLine($"PDF converted to PPTX: {outputPptxPath}");

                // ---------- Extract slide titles ----------
                // Assume the first line of each page represents the slide title
                using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header
                    csvWriter.WriteLine("SlideNumber,Title");

                    for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                    {
                        // Extract text from the current page
                        TextAbsorber absorber = new TextAbsorber();
                        pdfDoc.Pages[pageNum].Accept(absorber);
                        string pageText = absorber.Text ?? string.Empty;

                        // Determine title (first non‑empty line)
                        string title = string.Empty;
                        using (StringReader sr = new StringReader(pageText))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (!string.IsNullOrWhiteSpace(line))
                                {
                                    title = line.Trim();
                                    break;
                                }
                            }
                        }

                        // Escape double quotes in title for CSV compliance
                        title = title.Replace("\"", "\"\"");

                        csvWriter.WriteLine($"{pageNum},\"{title}\"");
                    }
                }

                Console.WriteLine($"Slide titles extracted to CSV: {outputCsvPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
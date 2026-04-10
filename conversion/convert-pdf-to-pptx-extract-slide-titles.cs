using System;
using System.IO;
using System.Text;
using Aspose.Pdf;                     // PDF handling, conversion, and text extraction
using Aspose.Pdf.Text;                // TextAbsorber for extracting page text

class PdfToPptxAndExtractTitles
{
    static void Main()
    {
        // Input PDF, intermediate PPTX, and output CSV paths
        const string pdfPath  = "input.pdf";
        const string pptxPath = "output.pptx";
        const string csvPath  = "slide_titles.csv";

        // -------------------------------------------------
        // 1. Convert PDF to PPTX using Aspose.Pdf
        // -------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (using ensures disposal)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Save the PDF directly as PPTX – no extra SaveOptions class needed
                pdfDoc.Save(pptxPath, SaveFormat.Pptx);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑PPTX conversion: {ex.Message}");
            return;
        }

        // -------------------------------------------------
        // 2. Extract slide titles (using PDF pages as slide equivalents)
        // -------------------------------------------------
        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                StringBuilder csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("SlideNumber,Title");

                // Iterate over each page – each page corresponds to a slide after conversion
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    // Extract all text from the page
                    TextAbsorber absorber = new TextAbsorber();
                    pdfDoc.Pages[i].Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Use the first non‑empty line as the slide title (fallback to empty string)
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

                    // Escape CSV special characters
                    string escapedTitle = title.Replace("\"", "\"\"");
                    if (escapedTitle.Contains(",") || escapedTitle.Contains("\n"))
                        escapedTitle = $"\"{escapedTitle}\"";

                    csvBuilder.AppendLine($"{i},{escapedTitle}");
                }

                File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
            }

            Console.WriteLine($"Slide titles extracted to CSV: {csvPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during slide title extraction: {ex.Message}");
        }
    }
}

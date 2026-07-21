using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string summaryPdfPath = "summary.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Extract text from the first three pages using PdfExtractor (Facades API)
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.StartPage = 1;   // 1‑based indexing
            extractor.EndPage   = 3;
            extractor.ExtractText();

            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                extractedText = Encoding.UTF8.GetString(textStream.ToArray());
            }
        }

        // Create a new PDF document containing the extracted text
        using (Document summaryDoc = new Document())
        {
            // Add a single page
            summaryDoc.Pages.Add();

            // Add the extracted text as a TextFragment
            TextFragment fragment = new TextFragment(extractedText);
            summaryDoc.Pages[1].Paragraphs.Add(fragment);

            // Save the summary PDF
            summaryDoc.Save(summaryPdfPath);
        }

        Console.WriteLine($"Summary PDF created: {summaryPdfPath}");
    }
}
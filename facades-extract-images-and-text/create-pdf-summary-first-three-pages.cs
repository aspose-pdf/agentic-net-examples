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
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "summary.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create a new empty PDF document that will hold the summary.
        using (Document summaryDoc = new Document())
        {
            // Use PdfExtractor (Facade API) to extract text from the first three pages.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);
                // Loop through pages 1 to 3 (Aspose.Pdf uses 1‑based indexing).
                for (int pageNumber = 1; pageNumber <= 3; pageNumber++)
                {
                    extractor.StartPage = pageNumber;
                    extractor.EndPage   = pageNumber;
                    extractor.ExtractText();

                    // Retrieve the extracted text into a memory stream.
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        textStream.Position = 0;
                        string pageText = new StreamReader(textStream, Encoding.UTF8).ReadToEnd();

                        // Add a new page to the summary document.
                        Page summaryPage = summaryDoc.Pages.Add();

                        // Create a TextFragment with the extracted text and add it to the page.
                        TextFragment fragment = new TextFragment(pageText);
                        // Optional: set some basic appearance.
                        fragment.TextState.FontSize = 12;
                        fragment.TextState.Font = FontRepository.FindFont("Helvetica");
                        fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                        summaryPage.Paragraphs.Add(fragment);
                    }
                }
            }

            // Save the summary PDF. No SaveOptions needed for PDF output.
            summaryDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Summary PDF created: {outputPdfPath}");
    }
}
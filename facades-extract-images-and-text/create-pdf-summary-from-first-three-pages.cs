using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF to obtain the total page count
        using (Document sourceDoc = new Document(inputPath))
        {
            int totalPages = sourceDoc.Pages.Count;
            int pageCount = Math.Min(3, totalPages);

            // Prepare a new PDF document that will hold the summary
            using (Document summaryDoc = new Document())
            {
                // Use PdfExtractor to extract text page‑by‑page
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(inputPath);

                    for (int i = 1; i <= pageCount; i++)
                    {
                        // Set the page range to a single page
                        extractor.StartPage = i;
                        extractor.EndPage   = i;
                        extractor.ExtractText();

                        // Get the extracted text for the current page via a stream
                        string pageText;
                        using (MemoryStream textStream = new MemoryStream())
                        {
                            extractor.GetText(textStream); // write text to the stream
                            textStream.Position = 0;
                            using (StreamReader reader = new StreamReader(textStream))
                            {
                                pageText = reader.ReadToEnd();
                            }
                        }

                        // Add a new page to the summary document
                        Page newPage = summaryDoc.Pages.Add();

                        // Create a TextFragment with the extracted text
                        TextFragment fragment = new TextFragment(pageText ?? string.Empty)
                        {
                            TextState = { FontSize = 12, Font = FontRepository.FindFont("Arial") }
                        };

                        // Add the fragment to the page's paragraphs collection
                        newPage.Paragraphs.Add(fragment);
                    }
                }

                // Save the summary PDF
                summaryDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Summary PDF created: {outputPath}");
    }
}

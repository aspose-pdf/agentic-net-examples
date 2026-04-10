using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Extract text from the first three pages using PdfExtractor (Facade API)
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPath);
                // The ExtractTextMode enum is not available in the current Aspose.Pdf version.
                // The default extraction mode works for most scenarios, so the line that set
                // ExtractTextMode has been removed.

                // Create a new PDF document for the summary
                using (Document summaryDoc = new Document())
                {
                    // Loop through pages 1 to 3 (Aspose.Pdf uses 1‑based indexing)
                    for (int pageNumber = 1; pageNumber <= 3; pageNumber++)
                    {
                        extractor.StartPage = pageNumber;
                        extractor.EndPage   = pageNumber;
                        extractor.ExtractText();

                        // Retrieve the extracted text into a memory stream
                        using (MemoryStream ms = new MemoryStream())
                        {
                            extractor.GetText(ms);
                            string pageText = System.Text.Encoding.UTF8.GetString(ms.ToArray());

                            // Add a new page to the summary document
                            Page summaryPage = summaryDoc.Pages.Add();

                            // Create a TextFragment with the extracted text
                            TextFragment tf = new TextFragment(pageText)
                            {
                                // Position the text at the top‑left corner of the page
                                Position = new Position(0, summaryPage.PageInfo.Height)
                            };

                            // Add the fragment to the page's paragraph collection
                            summaryPage.Paragraphs.Add(tf);
                        }
                    }

                    // Save the summary PDF
                    summaryDoc.Save(outputPath);
                }
            }

            Console.WriteLine($"Summary PDF created: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

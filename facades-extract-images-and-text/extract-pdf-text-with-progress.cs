using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large_input.pdf";
        const string outputTxtPath = "extracted_text.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the document to obtain the total page count (PdfExtractor does not expose this).
            Document doc = new Document(inputPdfPath);
            int pageCount = doc.Pages.Count;

            // PdfExtractor implements IDisposable – use a using block for deterministic disposal.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF.
                extractor.BindPdf(inputPdfPath);

                // Prepare the output file.
                using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
                {
                    // Process each page individually to report progress.
                    for (int page = 1; page <= pageCount; page++)
                    {
                        Console.WriteLine($"Processing page {page} of {pageCount}...");

                        // Configure the extractor to work on a single page.
                        extractor.StartPage = page;
                        extractor.EndPage   = page;

                        // Extract text from the current page.
                        extractor.ExtractText();

                        // Retrieve the extracted text via a stream (GetText requires a destination stream).
                        string pageText;
                        using (MemoryStream textStream = new MemoryStream())
                        {
                            extractor.GetText(textStream);
                            textStream.Position = 0;
                            using (StreamReader reader = new StreamReader(textStream))
                            {
                                pageText = reader.ReadToEnd();
                            }
                        }

                        // Write the page text to the output file (optional separator).
                        writer.WriteLine($"--- Page {page} ---");
                        writer.WriteLine(pageText);
                    }
                }

                Console.WriteLine($"Extraction completed. Text saved to '{outputTxtPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

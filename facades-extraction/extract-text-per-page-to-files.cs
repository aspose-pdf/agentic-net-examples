using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get total number of pages using PdfFileInfo (1‑based indexing)
        int pageCount = new Aspose.Pdf.Facades.PdfFileInfo(inputPdf).NumberOfPages;

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Iterate through each page, extract its text, and save to a file
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Restrict extraction to the current page only
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;

                // Perform the extraction for the selected page
                extractor.ExtractText();

                // Get the extracted text via a stream (GetText has no parameter‑less overload)
                string pageText;
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream); // writes text to the stream
                    textStream.Position = 0;       // rewind for reading
                    using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                    {
                        pageText = reader.ReadToEnd();
                    }
                }

                // Build output file name: Page_#.txt
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.txt");

                // Write the extracted text to the file
                File.WriteAllText(outputPath, pageText, Encoding.UTF8);
            }
        }

        Console.WriteLine("Per‑page text extraction completed.");
    }
}

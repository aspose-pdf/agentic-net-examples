using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PageTexts";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output subdirectory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Get total page count via Document (PdfExtractor has no PageCount property)
            Document pdfDoc = new Document(inputPdfPath);
            int pageCount = pdfDoc.Pages.Count;

            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF file once
                extractor.BindPdf(inputPdfPath);

                // Iterate over each page, set the range, extract and save text
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    extractor.StartPage = pageNumber; // first page to extract (1‑based)
                    extractor.EndPage   = pageNumber; // last page to extract
                    extractor.ExtractText();

                    // GetText writes the extracted text into a stream – read it back as a string
                    string pageText;
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        textStream.Position = 0;
                        using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                        {
                            pageText = reader.ReadToEnd();
                        }
                    }

                    string pageFile = Path.Combine(outputFolder, $"Page_{pageNumber}.txt");
                    File.WriteAllText(pageFile, pageText ?? string.Empty);
                }
            }

            Console.WriteLine($"Per‑page text extraction completed. Files saved in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}

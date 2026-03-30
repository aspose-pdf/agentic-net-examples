using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string keyword = "CONFIDENTIAL"; // keyword to search for

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Get total number of pages from the underlying Document
            Document doc = extractor.Document;
            int pageCount = doc.Pages.Count;

            // Iterate through each page, extract its text and check for the keyword
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Set the page range to a single page for text extraction
                extractor.StartPage = pageNumber;
                extractor.EndPage = pageNumber;
                extractor.ExtractText(Encoding.Unicode);

                // Retrieve the extracted text into a memory stream
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                    textStream.Position = 0;
                    using (StreamReader reader = new StreamReader(textStream))
                    {
                        string pageText = reader.ReadToEnd();
                        if (pageText.Contains(keyword))
                        {
                            // Keyword found – extract images from this page only
                            extractor.StartPage = pageNumber;
                            extractor.EndPage = pageNumber;
                            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
                            extractor.ExtractImage();

                            int imageIndex = 1;
                            while (extractor.HasNextImage())
                            {
                                string imageFileName = $"page{pageNumber}_image{imageIndex}.png";
                                extractor.GetNextImage(imageFileName);
                                imageIndex++;
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
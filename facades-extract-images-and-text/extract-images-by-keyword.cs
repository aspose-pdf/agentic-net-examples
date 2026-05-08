using System;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputDir  = "ExtractedImages";    // folder for images
        const string keyword    = "YOUR_KEYWORD";       // text to search for (case‑sensitive)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the document once to obtain the total page count.
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            // PdfExtractor is a disposable facade – use a using block.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor.
                extractor.BindPdf(inputPdf);

                // Iterate through each page.
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Restrict operations to the current page.
                    extractor.StartPage = pageNumber;
                    extractor.EndPage   = pageNumber;

                    // ----- STEP 1: extract text from the page -----
                    extractor.ExtractText();

                    // Capture the extracted text into a memory stream.
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        string pageText = Encoding.UTF8.GetString(textStream.ToArray());

                        // Check whether the page contains the target keyword.
                        if (pageText.Contains(keyword))
                        {
                            // ----- STEP 2: extract images from the same page -----
                            extractor.ExtractImage();

                            int imageIndex = 1;
                            while (extractor.HasNextImage())
                            {
                                string imagePath = Path.Combine(
                                    outputDir,
                                    $"page_{pageNumber}_img_{imageIndex}.png");

                                // Save the image as PNG (any ImageFormat is acceptable).
                                extractor.GetNextImage(imagePath, ImageFormat.Png);
                                imageIndex++;
                            }
                        }
                    }
                }

                // Release any resources held by the extractor.
                extractor.Close();
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
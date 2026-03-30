using System;
using System.IO;
using Aspose.Pdf.Facades;

public class PdfContentChecker
{
    public static bool ContainsTextAndImages(string pdfPath)
    {
        // Guard against a missing file – the extractor would throw a FileNotFoundException.
        if (!File.Exists(pdfPath))
        {
            return false;
        }

        bool hasText = false;
        bool hasImage = false;

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);

            // ----- Detect text -----
            extractor.ExtractText();
            // HasNextPageText indicates that the next page contains text.
            // We only need to know if *any* text exists, so break on the first hit.
            while (extractor.HasNextPageText())
            {
                // GetText requires a destination stream. We discard the content – we only need the call to succeed.
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                }
                hasText = true;
                break;
            }

            // ----- Detect images -----
            extractor.ExtractImage();
            while (extractor.HasNextImage())
            {
                // GetNextImage also requires a destination stream.
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                }
                hasImage = true;
                break;
            }
        }

        return hasText && hasImage;
    }

    public static void Main()
    {
        string inputPath = "sample.pdf";
        bool result = ContainsTextAndImages(inputPath);
        Console.WriteLine($"PDF contains both text and images: {result}");
    }
}

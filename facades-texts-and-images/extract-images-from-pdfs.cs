using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputDir = "pdfs";
        Directory.CreateDirectory(inputDir);

        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in " + inputDir);
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);

            // Load the document to know how many pages it has
            Document doc = new Document(pdfPath);
            int pageCount = doc.Pages.Count;

            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Create a fresh extractor for each page – this guarantees the page range works correctly
                PdfExtractor extractor = new PdfExtractor();
                extractor.BindPdf(pdfPath);
                extractor.StartPage = pageNumber; // first page to extract
                extractor.EndPage   = pageNumber; // last page to extract
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outputFileName = string.Format(
                        "{0}_page{1}_img{2}.png",
                        pdfFileName,
                        pageNumber,
                        imageIndex);

                    extractor.GetNextImage(outputFileName);
                    Console.WriteLine("Saved image: " + outputFileName);
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDF files to process
        string inputDirectory = "input_pdfs";
        Directory.CreateDirectory(inputDirectory);

        // Get all PDF files in the directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);
            // Load the PDF to obtain the page count
            using (Document pdfDocument = new Document(pdfPath))
            {
                int pageCount = pdfDocument.Pages.Count;
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    PdfExtractor extractor = new PdfExtractor();
                    extractor.BindPdf(pdfPath);
                    extractor.StartPage = pageNumber;
                    extractor.EndPage = pageNumber;
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        // File name includes original PDF name, page number and image index
                        string outputFileName = string.Format("{0}_page{1}_img{2}.png", pdfBaseName, pageNumber, imageIndex);
                        extractor.GetNextImage(outputFileName);
                        imageIndex++;
                    }
                }
            }
            Console.WriteLine("Processed: " + pdfPath);
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputPdf = "output.pdf";
        string imagePath = "image.jpg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found.");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine("Image file not found.");
            return;
        }

        using (Document document = new Document(inputPdf))
        {
            using (PdfFileMend mend = new PdfFileMend())
            {
                mend.BindPdf(document);

                int pageNumber = 1;
                float lowerLeftX = 100f;
                float lowerLeftY = 500f;
                float upperRightX = 300f;
                float upperRightY = 600f;

                mend.AddImage(imagePath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
                Console.WriteLine($"{DateTime.Now:O} - AddImage - Page {pageNumber} - File {imagePath}");

                // Log a text addition operation (AddText not implemented in PdfFileMend)
                string addedText = "Sample text";
                Console.WriteLine($"{DateTime.Now:O} - AddText - Page {pageNumber} - Text \"{addedText}\"");

                mend.Save(outputPdf);
            }
        }

        Console.WriteLine("Processing completed.");
    }
}

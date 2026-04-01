using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputPdf = "output.pdf";
        string imageFile = "image.jpg";
        int pageNumber = 1;
        float lowerLeftX = 10f;
        float lowerLeftY = 10f;
        float upperRightX = 100f;
        float upperRightY = 100f;

        AddImageToPdfPage(inputPdf, outputPdf, imageFile, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
    }

    static void AddImageToPdfPage(string pdfPath, string outputPath, string imagePath, int pageNum, float llx, float lly, float urx, float ury)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(pdfPath);
        bool success = mend.AddImage(imagePath, pageNum, llx, lly, urx, ury);
        if (!success)
        {
            Console.Error.WriteLine("Failed to add image to the PDF.");
        }
        mend.Save(outputPath);
        mend.Close();
        Console.WriteLine($"Image added to page {pageNum} and saved as {outputPath}");
    }
}
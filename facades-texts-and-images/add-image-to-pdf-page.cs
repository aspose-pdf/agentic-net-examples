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

        try
        {
            AddImageToPage(inputPdf, outputPdf, imageFile, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            Console.WriteLine("Image added successfully to page " + pageNumber);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }

    static void AddImageToPage(string pdfPath, string outputPath, string imagePath, int pageNumber, float lowerLeftX, float lowerLeftY, float upperRightX, float upperRightY)
    {
        if (!File.Exists(pdfPath))
        {
            throw new FileNotFoundException("PDF file not found: " + pdfPath);
        }
        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException("Image file not found: " + imagePath);
        }

        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(pdfPath);
        using (FileStream imageStream = File.OpenRead(imagePath))
        {
            bool success = mend.AddImage(imageStream, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            if (!success)
            {
                throw new InvalidOperationException("Failed to add image to PDF.");
            }
        }
        mend.Save(outputPath);
        mend.Close();
    }
}
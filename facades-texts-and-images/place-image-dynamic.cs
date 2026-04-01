using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string imagePath = "logo.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            float imageWidth = 100f;
            float imageHeight = 100f;

            float lowerLeftX = (float)(pageWidth * 0.1);
            float lowerLeftY = (float)(pageHeight * 0.1);
            float upperRightX = lowerLeftX + imageWidth;
            float upperRightY = lowerLeftY + imageHeight;

            PdfFileMend mend = new PdfFileMend(inputPath, outputPath);
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                mend.AddImage(imgStream, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            }
            mend.Close();
        }

        Console.WriteLine($"Image placed and saved to '{outputPath}'.");
    }
}

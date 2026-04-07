using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found.");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine("Image file not found.");
            return;
        }

        // Load PDF to obtain page dimensions
        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Desired image size (points)
            float imageWidth = 100.0f;
            float imageHeight = 100.0f;
            // Margin from page edges
            float margin = 20.0f;

            // Calculate lower‑left corner for bottom‑right placement
            float lowerLeftX = (float)(pageWidth - margin - imageWidth);
            float lowerLeftY = margin;
            float upperRightX = lowerLeftX + imageWidth;
            float upperRightY = lowerLeftY + imageHeight;

            // Add image using PdfFileMend facade
            PdfFileMend mend = new PdfFileMend(inputPath, outputPath);
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                bool success = mend.AddImage(imgStream, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
                if (!success)
                {
                    Console.Error.WriteLine("Failed to add image.");
                }
            }
            mend.Close();
        }

        Console.WriteLine("Image placed and saved to 'output.pdf'.");
    }
}

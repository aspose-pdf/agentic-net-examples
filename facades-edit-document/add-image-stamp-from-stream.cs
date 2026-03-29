using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath = "stamp.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdfPath);
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine("Image file not found: " + imagePath);
            return;
        }

        using (FileStream imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        {
            ImageStamp imageStamp = new ImageStamp(imageStream);
            imageStamp.Background = false;
            imageStamp.Opacity = 0.5f;
            imageStamp.XIndent = 100;
            imageStamp.YIndent = 200;
            // Optional size settings:
            // imageStamp.Width = 150;
            // imageStamp.Height = 100;

            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Apply the stamp to the first page (pages are 1‑based)
                pdfDocument.Pages[1].AddStamp(imageStamp);
                pdfDocument.Save(outputPdfPath);
            }
        }

        Console.WriteLine("Image stamp applied and saved to '" + outputPdfPath + "'.");
    }
}
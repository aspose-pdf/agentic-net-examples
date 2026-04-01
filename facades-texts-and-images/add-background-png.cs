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
        const string imagePath = "background.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine("Background image not found: " + imagePath);
            return;
        }

        // Load the PDF to obtain page count and dimensions
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Initialize PdfFileMend for output PDF
            PdfFileMend mend = new PdfFileMend(inputPath, outputPath);

            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];
                float lowerLeftX = 0f;
                float lowerLeftY = 0f;
                float upperRightX = (float)page.PageInfo.Width;
                float upperRightY = (float)page.PageInfo.Height;

                // Add the PNG image as background on the current page
                mend.AddImage(imagePath, i, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            }

            // Finalize the operation
            mend.Close();
        }

        Console.WriteLine("Background image added to all pages. Output saved to '" + outputPath + "'.");
    }
}

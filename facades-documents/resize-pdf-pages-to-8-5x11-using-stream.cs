using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // 8.5 x 11 inches expressed in points (1 inch = 72 points)
        const double widthPoints = 8.5 * 72;
        const double heightPoints = 11 * 72;

        // Use stream overload to resize pages
        using (FileStream inputStream = File.OpenRead(inputPath))
        using (FileStream outputStream = File.Create(outputPath))
        {
            // Load the PDF from the input stream
            Document pdfDocument = new Document(inputStream);

            // Resize each page to the specified dimensions using PageInfo
            foreach (Page page in pdfDocument.Pages)
            {
                page.PageInfo.Width = widthPoints;
                page.PageInfo.Height = heightPoints;
            }

            // Save the modified PDF to the output stream
            pdfDocument.Save(outputStream);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}

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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // 1 inch = 72 points
        const double inchToPoint = 72.0;
        double width = 8.5 * inchToPoint;   // 612 points
        double height = 11.0 * inchToPoint; // 792 points

        // Load the PDF from a stream (stream overload)
        using (FileStream inputStream = File.OpenRead(inputPath))
        using (Document pdf = new Document(inputStream))
        {
            // Resize each page to 8.5 x 11 inches using PageInfo
            foreach (Page page in pdf.Pages)
            {
                page.PageInfo.Width = width;
                page.PageInfo.Height = height;
            }

            // Save the modified PDF to an output stream
            using (FileStream outputStream = File.Create(outputPath))
            {
                pdf.Save(outputStream);
            }
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}

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

        // Desired dimensions (points; 1 point = 1/72 inch, assuming 72 DPI)
        const double targetWidth = 1024;   // points
        const double targetHeight = 768;   // points

        // Load PDF from a stream, resize each page, and save to a stream
        using (FileStream inputStream = File.OpenRead(inputPath))
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Load the document from the input stream
            Document doc = new Document(inputStream);

            // Resize every page via the PageInfo object
            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width = targetWidth;
                page.PageInfo.Height = targetHeight;
            }

            // Save the resized PDF to the output stream
            doc.Save(outputStream);

            // Persist the stream to a file
            File.WriteAllBytes(outputPath, outputStream.ToArray());
        }

        // Verify visual fidelity by checking the page dimensions of the saved PDF
        using (Document doc = new Document(outputPath))
        {
            double width = doc.Pages[1].PageInfo.Width;
            double height = doc.Pages[1].PageInfo.Height;

            Console.WriteLine($"Resized page size: {width} x {height} points");

            if (Math.Abs(width - 1024) < 0.1 && Math.Abs(height - 768) < 0.1)
            {
                Console.WriteLine("Resize verification passed.");
            }
            else
            {
                Console.WriteLine("Resize verification failed.");
            }
        }
    }
}

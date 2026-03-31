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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream memoryStream = new MemoryStream(pdfBytes))
        using (Document document = new Document(memoryStream))
        {
            // Resize each page to A4 (595 x 842 points)
            const float a4Width = 595f;
            const float a4Height = 842f;

            foreach (Page page in document.Pages)
            {
                page.PageInfo.Width = a4Width;
                page.PageInfo.Height = a4Height;
                page.PageInfo.IsLandscape = false;
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}

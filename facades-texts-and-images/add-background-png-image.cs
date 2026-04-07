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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        PdfFileMend mend = new PdfFileMend(inputPath, outputPath);
        for (int i = 1; i <= pageCount; i++)
        {
            // Add the image to the whole page (example coordinates)
            mend.AddImage(imagePath, i, 0f, 0f, 595f, 842f);
        }
        mend.Close();

        Console.WriteLine($"Background image added to {pageCount} pages and saved as '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;

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

        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Set orientation to landscape
                page.PageInfo.IsLandscape = true;
                // Apply A4 landscape dimensions (swap width and height)
                page.SetPageSize(Aspose.Pdf.PageSize.A4.Height, Aspose.Pdf.PageSize.A4.Width);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved landscape PDF to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string message = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];
            // Create a rectangle that spans the width of the page near the top
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                0,
                page.PageInfo.Height - 50,
                page.PageInfo.Width,
                page.PageInfo.Height);

            StampAnnotation stamp = new StampAnnotation(page, rect);
            stamp.Contents = message;
            stamp.Color = Aspose.Pdf.Color.Red;
            stamp.Opacity = 0.5f;
            stamp.TextHorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Top;

            page.Annotations.Add(stamp);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Line stamp added and saved to '{outputPath}'.");
    }
}

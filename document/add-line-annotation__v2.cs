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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 500);
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(100, 500);
            Aspose.Pdf.Point end = new Aspose.Pdf.Point(300, 500);

            LineAnnotation line = new LineAnnotation(page, rect, start, end);
            line.Color = Aspose.Pdf.Color.Gray;

            page.Annotations.Add(line);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}
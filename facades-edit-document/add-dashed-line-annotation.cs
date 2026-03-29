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

            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(120, 520);
            Aspose.Pdf.Point end = new Aspose.Pdf.Point(280, 530);

            LineAnnotation line = new LineAnnotation(page, rect, start, end);
            line.Color = Aspose.Pdf.Color.Blue;
            line.Border = new Border(line)
            {
                Width = 2,
                Dash = new Dash(new int[] { 4, 2 })
            };
            line.Contents = "Workflow step";

            page.Annotations.Add(line);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}
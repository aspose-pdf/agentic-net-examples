using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string url = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            LinkAnnotation link = new LinkAnnotation(page, rect);
            link.Action = new GoToURIAction(url);
            link.Color = Aspose.Pdf.Color.Blue;
            page.Annotations.Add(link);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink annotation added and saved to '{outputPath}'.");
    }
}
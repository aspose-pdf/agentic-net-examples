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
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 200, 200, 300);
            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Title = "Note";
            annotation.Contents = "Custom annotation content";
            annotation.Open = true;
            annotation.Icon = Aspose.Pdf.Annotations.TextIcon.Note;

            page.Annotations.Add(annotation);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}
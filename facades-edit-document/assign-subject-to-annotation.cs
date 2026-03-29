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
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                doc.Pages.Add();
            }

            Page page = doc.Pages[1];

            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Title = "Note";
            annotation.Contents = "This is a sample annotation.";
            annotation.Subject = "Review Note";

            page.Annotations.Add(annotation);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation with subject saved to '{outputPath}'.");
    }
}
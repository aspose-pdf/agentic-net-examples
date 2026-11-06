using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // Included as per requirement

class AnnotationPerformanceLogger
{
    private readonly Document _doc;

    public AnnotationPerformanceLogger(Document doc)
    {
        _doc = doc;
    }

    // Executes an annotation operation and logs its duration
    public void Execute(string operationName, Action operation)
    {
        Stopwatch sw = Stopwatch.StartNew();
        operation();
        sw.Stop();
        Console.WriteLine($"{operationName} took {sw.ElapsedMilliseconds} ms");
    }

    // Adds a text annotation to a specific page
    public void AddTextAnnotation(int pageNumber, Aspose.Pdf.Rectangle rect, string title, string contents)
    {
        Execute("AddTextAnnotation", () =>
        {
            Page page = _doc.Pages[pageNumber];
            TextAnnotation annot = new TextAnnotation(page, rect);
            annot.Title = title;
            annot.Contents = contents;
            annot.Open = true;
            annot.Icon = TextIcon.Note;
            page.Annotations.Add(annot);
        });
    }

    // Adds an image stamp to a specific page
    public void AddImageStamp(int pageNumber, string imagePath)
    {
        Execute("AddImageStamp", () =>
        {
            Page page = _doc.Pages[pageNumber];
            ImageStamp stamp = new ImageStamp(imagePath);
            stamp.Background = false;
            stamp.Opacity = 0.5;
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;
            page.AddStamp(stamp);
        });
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImage = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            var logger = new AnnotationPerformanceLogger(doc);

            // Add a text annotation to the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            logger.AddTextAnnotation(1, rect, "Note", "Performance monitored annotation.");

            // Add an image stamp to each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                logger.AddImageStamp(i, stampImage);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to {outputPath}");
    }
}
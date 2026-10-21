using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationPerformanceLogger
{
    // Executes an annotation operation and logs its duration.
    public static void Execute(Action operation, string operationName)
    {
        var stopwatch = Stopwatch.StartNew();
        operation();
        stopwatch.Stop();
        Console.WriteLine($"{operationName} completed in {stopwatch.ElapsedMilliseconds} ms");
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document – this is the object that supports annotations and stamps.
        Document doc = new Document(inputPath);

        // -------------------------------------------------
        // Example 1: Add a text annotation (performance‑monitored)
        // -------------------------------------------------
        AnnotationPerformanceLogger.Execute(
            () =>
            {
                // Rectangle that defines the annotation bounds.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create and configure the text annotation. Note the constructor now receives the target page and the rectangle.
                TextAnnotation textAnnotation = new TextAnnotation(doc.Pages[1], rect)
                {
                    Title    = "Note",
                    Contents = "This is a performance‑monitored text annotation.",
                    Open     = true,
                    Icon     = TextIcon.Note
                };

                // Add the annotation to the first page.
                doc.Pages[1].Annotations.Add(textAnnotation);
            },
            "AddTextAnnotation");

        // -------------------------------------------------
        // Example 2: Add an image stamp to every page (performance‑monitored)
        // -------------------------------------------------
        const string stampImagePath = "stamp.png";
        if (File.Exists(stampImagePath))
        {
            AnnotationPerformanceLogger.Execute(
                () =>
                {
                    // Create an image stamp.
                    ImageStamp imageStamp = new ImageStamp(stampImagePath)
                    {
                        Background          = false,
                        Opacity             = 0.7,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment   = VerticalAlignment.Center
                    };

                    // Apply the stamp to every page.
                    foreach (Page page in doc.Pages)
                    {
                        page.AddStamp(imageStamp);
                    }
                },
                "AddImageStamp");
        }
        else
        {
            Console.WriteLine($"Stamp image not found: {stampImagePath}. Skipping image stamp.");
        }

        // Save the modified PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}

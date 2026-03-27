using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        const int maxRetries = 5;
        const int initialDelayMs = 200;

        using (Document document = new Document(inputPath))
        {
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(document);

            // Create a concrete annotation type (TextAnnotation) – Annotation is abstract.
            // A rectangle is required for the annotation; values are arbitrary for the example.
            var rect = new Aspose.Pdf.Rectangle(100, 100, 200, 200);
            TextAnnotation annotation = new TextAnnotation(document.Pages[1], rect)
            {
                Title = "Updated Title",
                Contents = "Updated annotation contents.",
                Color = Aspose.Pdf.Color.Yellow
            };

            int attempt = 0;
            int delayMs = initialDelayMs;
            while (true)
            {
                try
                {
                    // Modify annotations on page 1 (page range is inclusive)
                    editor.ModifyAnnotations(1, 1, annotation);
                    break;
                }
                catch (IOException ioEx)
                {
                    attempt++;
                    if (attempt > maxRetries)
                    {
                        Console.Error.WriteLine("Failed to modify annotations after retries: " + ioEx.Message);
                        throw;
                    }
                    Console.WriteLine($"Transient error encountered, retrying in {delayMs} ms...");
                    Thread.Sleep(delayMs);
                    delayMs *= 2; // exponential back‑off
                }
            }

            document.Save(outputPath);
            Console.WriteLine($"Annotations modified and saved to '{outputPath}'.");
        }
    }
}

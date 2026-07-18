using System;
using System.IO;
using System.Threading;
using System.Drawing; // needed for Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        const int maxRetries = 5;
        int attempt = 0;
        int delayMs = 200;

        while (true)
        {
            try
            {
                // Load the PDF document and bind it to the annotation editor
                using (Document doc = new Document(inputPath))
                using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
                {
                    annotationEditor.BindPdf(doc);

                    // Example modification: delete all existing annotations
                    annotationEditor.DeleteAnnotations();

                    // Add a free‑text annotation on the first page using PdfContentEditor
                    using (PdfContentEditor contentEditor = new PdfContentEditor())
                    {
                        contentEditor.BindPdf(doc);
                        // System.Drawing.Rectangle expects (x, y, width, height)
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 50);
                        contentEditor.CreateFreeText(rect, "Sample annotation", 0);
                    }

                    // Save the modified document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
                break; // success
            }
            catch (IOException ex) when (IsTransient(ex))
            {
                attempt++;
                if (attempt > maxRetries)
                {
                    Console.Error.WriteLine($"Transient error persisted after {maxRetries} retries: {ex.Message}");
                    break;
                }

                Console.WriteLine($"Transient error (attempt {attempt}), retrying after {delayMs} ms...");
                Thread.Sleep(delayMs);
                delayMs *= 2; // exponential backoff
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }
    }

    // Simple heuristic: treat all IOExceptions as transient for this example
    static bool IsTransient(IOException ex) => true;
}

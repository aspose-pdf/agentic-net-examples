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
        const string logPath = "deletion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            using (StreamWriter logWriter = new StreamWriter(logPath, false))
            {
                // Write CSV header
                logWriter.WriteLine("AnnotationName,AnnotationType,PageNumber");

                // Pages are 1‑based
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    AnnotationCollection annotations = page.Annotations;

                    // Copy current annotations to an array to avoid modifying the collection while iterating
                    Annotation[] annotationArray = new Annotation[annotations.Count];
                    for (int i = 0; i < annotations.Count; i++)
                    {
                        annotationArray[i] = annotations[i];
                    }

                    foreach (Annotation annotation in annotationArray)
                    {
                        string name = annotation.Name ?? string.Empty;
                        string type = annotation.GetType().Name;
                        int pageNumber = pageIndex;

                        // Log deletion details
                        logWriter.WriteLine($"{name},{type},{pageNumber}");

                        // Delete the annotation from the page
                        annotations.Delete(annotation);
                    }
                }
            }

            // Save the PDF without the deleted annotations
            doc.Save(outputPath);
        }

        Console.WriteLine("Annotations deleted and logged to " + logPath);
    }
}

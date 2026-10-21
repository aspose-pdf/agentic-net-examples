using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
// Note: Annotation types are in the Aspose.Pdf.Annotations namespace.

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

        // Load the PDF document
        Document doc = new Document(inputPath);

        bool deletionSucceeded = false;
        int attemptCount = 0;
        const int maxAttempts = 2; // initial try + one retry

        while (!deletionSucceeded && attemptCount < maxAttempts)
        {
            try
            {
                // Iterate over all pages and delete their annotations
                foreach (Page page in doc.Pages)
                {
                    // The Annotations property returns an AnnotationCollection defined in Aspose.Pdf.Annotations
                    Aspose.Pdf.Annotations.AnnotationCollection annotations = page.Annotations;

                    // Collect annotations to delete (example: delete all)
                    List<Aspose.Pdf.Annotations.Annotation> toRemove = new List<Aspose.Pdf.Annotations.Annotation>();
                    foreach (Aspose.Pdf.Annotations.Annotation ann in annotations)
                    {
                        toRemove.Add(ann);
                    }

                    // Delete collected annotations
                    foreach (Aspose.Pdf.Annotations.Annotation ann in toRemove)
                    {
                        annotations.Delete(ann);
                    }
                }

                deletionSucceeded = true; // no exception, operation succeeded
            }
            catch (Exception ex)
            {
                // Log the failure and attempt to rebind (reload) the document
                Console.Error.WriteLine($"Attempt {attemptCount + 1} failed: {ex.Message}");
                attemptCount++;

                // Rebind the PDF by reloading it from the original file
                doc.Dispose();
                doc = new Document(inputPath);
            }
        }

        if (!deletionSucceeded)
        {
            Console.Error.WriteLine("Failed to delete annotations after retry.");
        }
        else
        {
            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Annotations deleted and PDF saved to '{outputPath}'.");
        }

        // Ensure the document is disposed
        doc.Dispose();
    }
}

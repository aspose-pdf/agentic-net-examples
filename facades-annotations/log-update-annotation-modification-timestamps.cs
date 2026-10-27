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
        const string logPath = "annotation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the log file is empty before we start
        File.WriteAllText(logPath, string.Empty);

        // Load the PDF document inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Each page may contain multiple annotations
                foreach (Annotation annotation in page.Annotations)
                {
                    // Only markup annotations have a modification timestamp property
                    if (annotation is MarkupAnnotation markup)
                    {
                        // Update the modification timestamp (use Modified property, not ModifiedDate)
                        markup.Modified = DateTime.UtcNow;

                        // Log the annotation type and its new timestamp
                        string logEntry = $"Page {i}, Annotation {annotation.GetType().Name}, Modified = {markup.Modified:O}";
                        File.AppendAllText(logPath, logEntry + Environment.NewLine);
                        Console.WriteLine(logEntry);
                    }
                    else
                    {
                        // For non‑markup annotations just log that they were inspected
                        string logEntry = $"Page {i}, Annotation {annotation.GetType().Name} (no modification timestamp)";
                        File.AppendAllText(logPath, logEntry + Environment.NewLine);
                        Console.WriteLine(logEntry);
                    }
                }
            }

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Modified PDF saved to '{outputPath}'. Log written to '{logPath}'.");
    }
}

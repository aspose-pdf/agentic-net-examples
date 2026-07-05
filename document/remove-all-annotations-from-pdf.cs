using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based (global rule)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Delete all annotations on each page (AnnotationCollection.Delete)
                    doc.Pages[i].Annotations.Delete();
                }

                // Save the cleaned PDF (lifecycle rule: use Document.Save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"All annotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
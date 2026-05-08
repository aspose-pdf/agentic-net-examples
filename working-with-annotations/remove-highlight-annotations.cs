using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate through each page (1‑based indexing)
                foreach (Page page in doc.Pages)
                {
                    // Delete highlight annotations.
                    // Iterate backwards to avoid index shifting after deletion.
                    for (int idx = page.Annotations.Count; idx >= 1; idx--)
                    {
                        Annotation annot = page.Annotations[idx];
                        if (annot is HighlightAnnotation)
                        {
                            page.Annotations.Delete(idx);
                        }
                    }
                }

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All highlight annotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
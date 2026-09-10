using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_screen.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Collect ScreenAnnotation instances to remove
                var toRemove = new List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is ScreenAnnotation)
                        toRemove.Add(ann);
                }

                // Delete the collected ScreenAnnotations
                foreach (Annotation ann in toRemove)
                {
                    page.Annotations.Delete(ann);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Screen annotations removed. Saved to '{outputPath}'.");
    }
}
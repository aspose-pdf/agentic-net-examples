using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                int fieldCount = 0;

                // Form fields are represented by WidgetAnnotation objects
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is WidgetAnnotation)
                        fieldCount++;
                }

                // Log the number of fields extracted from the current page
                Console.WriteLine($"Page {i}: extracted {fieldCount} form field(s).");
            }
        }
    }
}
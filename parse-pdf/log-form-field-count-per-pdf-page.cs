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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Count form fields on the current page.
                // Form fields are represented by WidgetAnnotation objects.
                int fieldCount = 0;
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is WidgetAnnotation)
                        fieldCount++;
                }

                Console.WriteLine($"Page {i}: {fieldCount} form field(s) extracted.");
            }
        }
    }
}
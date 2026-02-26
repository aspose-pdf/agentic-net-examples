using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string annotationName = "MyNote"; // name of the annotation to retrieve

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                AnnotationCollection annots = page.Annotations;

                // Try to find the annotation by its Name property
                Annotation found = annots.FindByName(annotationName);
                if (found != null)
                {
                    Console.WriteLine($"Annotation found on page {i}:");
                    Console.WriteLine($"  Type   : {found.AnnotationType}");
                    Console.WriteLine($"  Name   : {found.Name}");
                    Console.WriteLine($"  Contents: {found.Contents}");
                    // If it's a TextAnnotation you can cast to access specific properties
                    if (found is TextAnnotation textAnn)
                    {
                        Console.WriteLine($"  Title  : {textAnn.Title}");
                    }
                    // Stop after the first match; remove this break to find all occurrences
                    break;
                }
            }
        }
    }
}
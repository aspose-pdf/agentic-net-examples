using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string annotationName = "MyNote";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Search each page for the annotation with the specified name
            foreach (Page page in doc.Pages)
            {
                // AnnotationCollection uses 1‑based indexing
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    if (string.Equals(ann.Name, annotationName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Example handling for a TextAnnotation
                        if (ann is TextAnnotation textAnn)
                        {
                            Console.WriteLine($"Found TextAnnotation on page {page.Number}");
                            Console.WriteLine($"Title   : {textAnn.Title}");
                            Console.WriteLine($"Contents: {textAnn.Contents}");
                        }
                        else
                        {
                            Console.WriteLine($"Found annotation of type {ann.GetType().Name} on page {page.Number}");
                        }

                        // Stop after the first match
                        return;
                    }
                }
            }

            Console.WriteLine($"Annotation named '{annotationName}' was not found.");
        }
    }
}
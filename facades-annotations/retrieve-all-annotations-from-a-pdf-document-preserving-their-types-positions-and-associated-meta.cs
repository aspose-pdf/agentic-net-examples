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

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Iterate through each page and its annotations
        foreach (Page page in pdfDocument.Pages)
        {
            foreach (Annotation annotation in page.Annotations)
            {
                // Basic information available on the base Annotation class
                string typeName = annotation.AnnotationType.ToString();
                Aspose.Pdf.Rectangle rect = annotation.Rect;
                string contents = annotation.Contents ?? string.Empty;
                Aspose.Pdf.Color color = annotation.Color;

                // Type‑specific information – accessed only after a safe cast
                string title = string.Empty;
                string subject = string.Empty;
                string open = string.Empty;

                // Title and Subject are defined on MarkupAnnotation (base for most markup types)
                if (annotation is MarkupAnnotation markup)
                {
                    title = markup.Title ?? string.Empty;
                    subject = markup.Subject ?? string.Empty;
                }

                // The Open property exists on TextAnnotation (a derived markup type)
                if (annotation is TextAnnotation textAnnot)
                {
                    open = textAnnot.Open.ToString();
                }

                // Output the collected data
                Console.WriteLine($"Page: {page.Number}, Type: {typeName}");
                Console.WriteLine($"  Rect: [{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}]");
                if (!string.IsNullOrEmpty(title))
                    Console.WriteLine($"  Title: {title}");
                if (!string.IsNullOrEmpty(contents))
                    Console.WriteLine($"  Contents: {contents}");
                if (!string.IsNullOrEmpty(subject))
                    Console.WriteLine($"  Subject: {subject}");
                if (!string.IsNullOrEmpty(open))
                    Console.WriteLine($"  Open: {open}");
                Console.WriteLine($"  Color: {(color != null ? color.ToString() : "None")}");
                Console.WriteLine();
            }
        }
    }
}

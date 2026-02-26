using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputSvg = "output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Add a TextAnnotation (sticky note) to the first page
            // -------------------------------------------------
            Aspose.Pdf.Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle where the annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 800);

            // Create the annotation
            Aspose.Pdf.Annotations.TextAnnotation textAnno = new Aspose.Pdf.Annotations.TextAnnotation(page, rect)
            {
                Title   = "John Doe",               // Title shown in the popup window
                Subject = "Review Comment",         // Subject/description
                Contents = "Please check this paragraph for accuracy.", // Main text
                Open    = true,                     // Open by default
                Icon    = Aspose.Pdf.Annotations.TextIcon.Note // Icon style
            };

            // Optional: customize the border
            Aspose.Pdf.Annotations.Border border = new Aspose.Pdf.Annotations.Border(textAnno)
            {
                Width = 1,
                Dash  = new Aspose.Pdf.Annotations.Dash(2, 2) // dashed line
            };
            textAnno.Border = border;

            // Add the annotation to the page
            page.Annotations.Add(textAnno);

            // -------------------------------------------------
            // 2. Retrieve and display all markup annotations on the first page
            // -------------------------------------------------
            Console.WriteLine("Annotations on page 1 after addition:");
            foreach (Aspose.Pdf.Annotations.Annotation anno in page.Annotations)
            {
                // Only markup annotations have Title/Subject/Contents
                if (anno is Aspose.Pdf.Annotations.MarkupAnnotation markup)
                {
                    Console.WriteLine($"- Title:   {markup.Title}");
                    Console.WriteLine($"  Subject: {markup.Subject}");
                    Console.WriteLine($"  Contents:{markup.Contents}");
                }
            }

            // -------------------------------------------------
            // 3. Delete the previously added annotation (first in the collection)
            // -------------------------------------------------
            if (page.Annotations.Count > 0)
            {
                // Remove by reference
                page.Annotations.Remove(textAnno);
                // Alternatively, you could use page.Annotations.Delete(0);
            }

            // Verify deletion
            Console.WriteLine("\nAnnotations on page 1 after deletion:");
            foreach (Aspose.Pdf.Annotations.Annotation anno in page.Annotations)
            {
                if (anno is Aspose.Pdf.Annotations.MarkupAnnotation markup)
                {
                    Console.WriteLine($"- Title: {markup.Title}");
                }
            }

            // -------------------------------------------------
            // 4. Save the modified document as SVG (requires explicit save options)
            // -------------------------------------------------
            Aspose.Pdf.SvgSaveOptions svgOptions = new Aspose.Pdf.SvgSaveOptions();
            doc.Save(outputSvg, svgOptions);
        }

        Console.WriteLine($"\nProcessing complete. SVG saved to '{outputSvg}'.");
    }
}
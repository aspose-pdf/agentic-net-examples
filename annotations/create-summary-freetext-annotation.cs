using System;
using System.IO;
using System.Text;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("Document does not contain a second page.");
                return;
            }

            // Retrieve all annotations on page 2 (1‑based indexing)
            Page pageTwo = doc.Pages[2];
            AnnotationCollection annotations = pageTwo.Annotations;

            // Collect the Title (if available) or Contents of each annotation
            StringBuilder subjects = new StringBuilder();
            foreach (Annotation ann in annotations)
            {
                string subject = null;
                // Title exists only on markup‑type annotations
                if (ann is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                {
                    subject = markup.Title;
                }
                else if (!string.IsNullOrEmpty(ann.Contents))
                {
                    subject = ann.Contents;
                }

                if (!string.IsNullOrEmpty(subject))
                {
                    if (subjects.Length > 0) subjects.Append("; ");
                    subjects.Append(subject);
                }
            }

            // Prepare the summary text for the FreeTextAnnotation
            string summary = subjects.Length > 0
                ? $"Annotations subjects: {subjects}"
                : "No annotation subjects found on page 2.";

            // Define a rectangle for the FreeTextAnnotation (positioned near the top of the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 550, 800);

            // Build the DefaultAppearance required by FreeTextAnnotation
            DefaultAppearance appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation and set its properties
            var freeText = new FreeTextAnnotation(pageTwo, rect, appearance)
            {
                Title = "Summary",
                Contents = summary,
                Color = Aspose.Pdf.Color.LightYellow, // background colour
                Justification = Justification.Left
            };

            // Add a thin border around the annotation
            freeText.Border = new Border(freeText) { Width = 1 };

            // Add the new annotation to page 2
            pageTwo.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}

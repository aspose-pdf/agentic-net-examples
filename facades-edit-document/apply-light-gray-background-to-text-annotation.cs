using System;
using System.IO;
using System.Drawing;                     // needed for System.Drawing.Rectangle and Color
using Aspose.Pdf;                         // core PDF classes
using Aspose.Pdf.Facades;                 // facades API (PdfContentEditor)
using Aspose.Pdf.Annotations;             // TextAnnotation type

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the facades editor and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the annotation rectangle using System.Drawing.Rectangle (required by PdfContentEditor)
            // System.Drawing.Rectangle ctor: (x, y, width, height)
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a text (sticky‑note) annotation on page 1
            // Parameters: rectangle, contents, title, open flag, author, page number
            editor.CreateText(annotRect, "This is a helpful note.", "Note", true, "Author", 1);

            // After creation, retrieve the newly added annotation to set its background color
            // Annotations collection uses 1‑based indexing
            Page page = doc.Pages[1];
            if (page.Annotations.Count > 0)
            {
                // Assuming the last annotation added is the one we just created
                Annotation ann = page.Annotations[page.Annotations.Count];
                if (ann is TextAnnotation textAnn)
                {
                    // Set the annotation's background (popup) color to light gray
                    textAnn.Color = Aspose.Pdf.Color.LightGray;
                }
            }

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}

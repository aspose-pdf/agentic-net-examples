using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_filtered.pdf";

        // Define the RGB color to match (e.g., pure red)
        Aspose.Pdf.Color targetColor = Aspose.Pdf.Color.FromArgb(255, 0, 0);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the PdfAnnotationEditor (facade API)
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // Delete matching annotations by iterating backwards to avoid index shift
                for (int i = annotations.Count; i >= 1; i--)
                {
                    Annotation annot = annotations[i];
                    // Compare the annotation's Color with the target RGB value
                    if (annot.Color != null && annot.Color.Equals(targetColor))
                    {
                        // Remove the annotation from the collection
                        annotations.Delete(i);
                    }
                }
            }

            // Save the modified PDF using the facade's Save method (lifecycle rule)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Filtered PDF saved to '{outputPath}'.");
    }
}
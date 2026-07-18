using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;   // for Aspose.Pdf.Color

public class AnnotationCloner
{
    /// <summary>
    /// Clones the first annotation on the first page, modifies some of its properties,
    /// adds the cloned annotation back to the same page and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    public static void CloneModifyAndAdd(string inputPdfPath, string outputPdfPath)
    {
        // Load the PDF document (creation & loading rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page and one annotation
            if (doc.Pages.Count == 0 || doc.Pages[1].Annotations.Count == 0)
                throw new InvalidOperationException("Document must contain at least one annotation on page 1.");

            // Get the first page and its first annotation (annotation collections are 1‑based)
            Page page = doc.Pages[1];
            Annotation original = page.Annotations[1];

            // Clone the annotation based on its concrete type.
            // Annotation.Clone() always returns null, so we create a new instance manually.
            Annotation cloned;

            // Example for TextAnnotation – similar blocks can be added for other types if needed.
            if (original is TextAnnotation textAnno)
            {
                // Create a new TextAnnotation on the same page with the same rectangle.
                var clonedText = new TextAnnotation(page, textAnno.Rect);

                // Copy common properties that belong to MarkupAnnotation / TextAnnotation.
                clonedText.Title    = textAnno.Title;
                clonedText.Contents = textAnno.Contents;
                clonedText.Color    = textAnno.Color;
                clonedText.Modified = textAnno.Modified;
                clonedText.Subject  = textAnno.Subject;
                clonedText.Open     = textAnno.Open;

                // Modify properties as required.
                clonedText.Title    = "Cloned Title";
                clonedText.Contents = "This is a cloned and modified annotation.";
                clonedText.Color    = Color.Green;   // Use Aspose.Pdf.Color (cross‑platform)

                cloned = clonedText; // assign to base variable for collection add
            }
            else if (original is StampAnnotation stampAnno)
            {
                // Create a new StampAnnotation on the same page.
                var clonedStamp = new StampAnnotation(page, stampAnno.Rect);

                // StampAnnotation does not expose Title/Subject/Open – set only supported members.
                clonedStamp.Contents = "Cloned stamp annotation.";
                clonedStamp.Color    = Color.Blue;
                clonedStamp.Modified = DateTime.Now;

                cloned = clonedStamp;
            }
            else
            {
                // For unsupported annotation types, throw an informative exception.
                throw new NotSupportedException($"Cloning of annotation type '{original.GetType().Name}' is not implemented.");
            }

            // Add the cloned annotation back to the page.
            page.Annotations.Add(cloned);

            // Save the modified document using the Facades API (save rule).
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);               // Initialize the facade with the document.
            editor.Save(outputPdfPath);        // Persist changes.
            editor.Close();                    // Release resources (PdfAnnotationEditor does not implement IDisposable).
        }
    }

    // Example usage
    public static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cloned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            CloneModifyAndAdd(inputPath, outputPath);
            Console.WriteLine($"Cloned annotation saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

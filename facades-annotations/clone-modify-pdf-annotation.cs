using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

public static class AnnotationHelper
{
    /// <summary>
    /// Clones the annotation at the specified index on the given page, modifies some of its properties,
    /// and adds the cloned annotation back to the same page.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    /// <param name="pageNumber">1‑based page number containing the annotation.</param>
    /// <param name="annotationIndex">1‑based index of the annotation in the page's annotation collection.</param>
    public static void CloneModifyAdd(string inputPdfPath, string outputPdfPath, int pageNumber, int annotationIndex)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdfPath))
        {
            // Validate page number.
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number: {pageNumber}");
                return;
            }

            // Get the target page.
            Page page = doc.Pages[pageNumber];

            // Validate annotation index.
            if (annotationIndex < 1 || annotationIndex > page.Annotations.Count)
            {
                Console.Error.WriteLine($"Invalid annotation index: {annotationIndex}");
                return;
            }

            // Retrieve the original annotation.
            Annotation original = page.Annotations[annotationIndex];

            // Clone and modify the annotation.
            Annotation cloned = CloneAndModify(original, page);

            if (cloned != null)
            {
                // Add the cloned annotation to the same page.
                page.Annotations.Add(cloned);
                // Save the updated document.
                doc.Save(outputPdfPath);
                Console.WriteLine($"Cloned annotation added and document saved to '{outputPdfPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Cloning failed: unsupported annotation type.");
            }
        }
    }

    /// <summary>
    /// Creates a copy of the supplied annotation, applies simple modifications,
    /// and returns the new annotation instance.
    /// </summary>
    /// <param name="source">The annotation to clone.</param>
    /// <param name="page">The page on which the new annotation will be placed.</param>
    /// <returns>A new annotation instance with modified properties, or null if the type is unsupported.</returns>
    private static Annotation CloneAndModify(Annotation source, Page page)
    {
        // Most annotation types expose a constructor that accepts (Page, Rectangle).
        // We'll handle the most common markup types: TextAnnotation, LinkAnnotation and StampAnnotation.
        // For other types you can extend this method similarly.

        if (source is TextAnnotation textAnno)
        {
            // Create a new TextAnnotation on the same page with the same rectangle.
            TextAnnotation clone = new TextAnnotation(page, textAnno.Rect)
            {
                // Modify visual properties.
                Color = Color.Red,                     // Change color to red.
                // Append a suffix to the existing contents.
                Contents = (textAnno.Contents ?? string.Empty) + " (cloned)",
                // Update the title to indicate it is a clone.
                Title = "Cloned Annotation",
                // Preserve other useful fields.
                Modified = DateTime.Now,
                Open = true,
                Subject = textAnno.Subject,
                // Copy border if needed.
                Border = textAnno.Border
            };
            return clone;
        }
        else if (source is LinkAnnotation linkAnno)
        {
            // Create a new LinkAnnotation on the same page with the same rectangle.
            LinkAnnotation clone = new LinkAnnotation(page, linkAnno.Rect)
            {
                Color = Color.Blue,                    // Change color to blue.
                Contents = (linkAnno.Contents ?? string.Empty) + " (cloned)",
                // Preserve the original action if present.
                Action = linkAnno.Action,
                // Subject property does not exist on LinkAnnotation in current API – omit it.
                Border = linkAnno.Border
            };
            return clone;
        }
        else if (source is StampAnnotation stampAnno)
        {
            // Create a new StampAnnotation on the same page.
            StampAnnotation clone = new StampAnnotation(page, stampAnno.Rect)
            {
                Color = Color.Green,
                Contents = (stampAnno.Contents ?? string.Empty) + " (cloned)",
                Title = "Cloned Stamp",
                Subject = stampAnno.Subject,
                Border = stampAnno.Border,
                Opacity = stampAnno.Opacity
            };
            return clone;
        }

        // If the annotation type is not supported, return null.
        return null;
    }

    // ---------------------------------------------------------------------
    // Entry point – required for a console‑style project.
    // ---------------------------------------------------------------------
    public static void Main(string[] args)
    {
        // Simple demonstration (can be removed or replaced by unit tests).
        if (args.Length == 4 &&
            int.TryParse(args[2], out int pageNum) &&
            int.TryParse(args[3], out int annoIdx))
        {
            CloneModifyAdd(args[0], args[1], pageNum, annoIdx);
        }
        else
        {
            Console.WriteLine("Usage: <inputPdf> <outputPdf> <pageNumber> <annotationIndex>");
        }
    }
}

// Example usage (uncomment for quick test):
// AnnotationHelper.CloneModifyAdd("input.pdf", "output.pdf", 1, 1);
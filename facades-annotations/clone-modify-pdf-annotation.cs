using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

public class AnnotationCloner
{
    /// <summary>
    /// Clones the first annotation on the first page, modifies some of its properties,
    /// and adds the cloned annotation back to the same page.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    public static void CloneModifyAndAdd(string inputPdfPath, string outputPdfPath)
    {
        // Ensure the source file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule).
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize the PdfAnnotationEditor facade and bind it to the loaded document.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Work with the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // If there are no annotations on the page, nothing to clone.
            if (page.Annotations.Count == 0)
            {
                Console.WriteLine("No annotations found on the first page.");
                editor.Save(outputPdfPath); // Save unchanged document.
                return;
            }

            // Retrieve the first annotation (1‑based collection).
            Annotation original = page.Annotations[1];

            // Prepare a variable for the cloned annotation.
            Annotation clonedAnnotation = null;

            // Handle TextAnnotation cloning – this can be extended for other types.
            if (original is TextAnnotation textAnno)
            {
                // Create a new TextAnnotation on the same page with the same rectangle.
                clonedAnnotation = new TextAnnotation(page, textAnno.Rect);

                // Copy common properties.
                clonedAnnotation.Color = textAnno.Color;
                clonedAnnotation.Contents = textAnno.Contents;
                // Title and Subject belong to MarkupAnnotation, so cast before setting.
                if (clonedAnnotation is MarkupAnnotation markupClone)
                {
                    markupClone.Title = textAnno.Title;
                    markupClone.Subject = textAnno.Subject;
                }
                clonedAnnotation.Modified = DateTime.Now;

                // Modify desired properties (example: change color and contents).
                clonedAnnotation.Color = Aspose.Pdf.Color.Red;
                clonedAnnotation.Contents = "Cloned and modified annotation";
            }
            // Example handling for StampAnnotation – similar copying can be added.
            else if (original is StampAnnotation stampAnno)
            {
                clonedAnnotation = new StampAnnotation(page, stampAnno.Rect);
                clonedAnnotation.Color = stampAnno.Color;
                clonedAnnotation.Contents = stampAnno.Contents;
                if (clonedAnnotation is MarkupAnnotation markupClone)
                {
                    markupClone.Title = stampAnno.Title;
                    markupClone.Subject = stampAnno.Subject;
                }
                clonedAnnotation.Modified = DateTime.Now;

                // Modify properties.
                clonedAnnotation.Color = Aspose.Pdf.Color.Green;
                clonedAnnotation.Contents = "Cloned stamp annotation";
            }
            else
            {
                // For unsupported annotation types, exit gracefully.
                Console.WriteLine($"Annotation type '{original.GetType().Name}' is not supported for cloning.");
                editor.Save(outputPdfPath); // Save unchanged document.
                return;
            }

            // Add the cloned annotation back to the same page.
            page.Annotations.Add(clonedAnnotation);

            // Save the modified document using the facade (ensures any facade‑related changes are persisted).
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Cloned annotation saved to '{outputPdfPath}'.");
    }

    // Entry point required for a console application.
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: AnnotationCloner <input-pdf-path> <output-pdf-path>");
            return;
        }

        CloneModifyAndAdd(args[0], args[1]);
    }
}

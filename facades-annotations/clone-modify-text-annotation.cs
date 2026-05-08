using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

public static class AnnotationHelper
{
    /// <summary>
    /// Clones the first TextAnnotation on the first page, modifies its properties,
    /// and adds the cloned annotation back to the same page.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="outputPdf">Path where the modified PDF will be saved.</param>
    public static void CloneModifyAdd(string inputPdf, string outputPdf)
    {
        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Bind the PDF to the PdfAnnotationEditor facade (required by the task).
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdf);

                // Work with the first page (Aspose.Pdf uses 1‑based indexing).
                Page page = doc.Pages[1];

                // Ensure there is at least one annotation to clone.
                if (page.Annotations.Count == 0)
                {
                    // No annotations present – nothing to clone.
                    doc.Save(outputPdf);
                    return;
                }

                // Retrieve the first annotation on the page.
                Annotation original = page.Annotations[1];

                // We only support cloning of TextAnnotation in this example.
                if (original is TextAnnotation txtOriginal)
                {
                    // The TextAnnotation constructor expects a Page and a Rectangle.
                    TextAnnotation clone = new TextAnnotation(page, txtOriginal.Rect);

                    // Copy and modify common properties.
                    clone.Title = (txtOriginal.Title ?? string.Empty) + " – Clone";
                    clone.Contents = (txtOriginal.Contents ?? string.Empty) + " (cloned)";
                    clone.Color = Color.Green; // Aspose.Pdf.Drawing.Color

                    // Add the cloned annotation back to the same page.
                    page.Annotations.Add(clone);
                }
                else
                {
                    // For unsupported annotation types, simply save the original document.
                    doc.Save(outputPdf);
                    return;
                }
            }

            // Save the updated document.
            doc.Save(outputPdf);
        }
    }
}

// A minimal entry point so the project compiles when built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the class library can be used directly.
        // Example usage (uncomment to test):
        // AnnotationHelper.CloneModifyAdd("input.pdf", "output.pdf");
    }
}
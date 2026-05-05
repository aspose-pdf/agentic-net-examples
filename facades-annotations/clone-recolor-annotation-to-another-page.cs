using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

public class AnnotationHelper
{
    /// <summary>
    /// Clones the first annotation from <paramref name="sourcePageNumber"/>, changes its color,
    /// and adds the cloned annotation to <paramref name="targetPageNumber"/>.
    /// The resulting PDF is saved to <paramref name="outputPath"/>.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the modified PDF will be saved.</param>
    /// <param name="sourcePageNumber">1‑based index of the page containing the original annotation.</param>
    /// <param name="targetPageNumber">1‑based index of the page where the cloned annotation will be placed.</param>
    /// <param name="newColor">The color to apply to the cloned annotation.</param>
    public static void CloneAndRecolorAnnotation(
        string inputPath,
        string outputPath,
        int sourcePageNumber,
        int targetPageNumber,
        Aspose.Pdf.Color newColor)
    {
        // Validate page numbers (Aspose.Pdf uses 1‑based indexing)
        if (sourcePageNumber < 1 || targetPageNumber < 1)
            throw new ArgumentException("Page numbers must be 1 or greater.");

        // Initialize the facade and bind the PDF document.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Access the underlying Document object.
        Document doc = editor.Document;

        // Ensure the requested pages exist.
        if (sourcePageNumber > doc.Pages.Count || targetPageNumber > doc.Pages.Count)
            throw new ArgumentException("Specified page number exceeds document page count.");

        // Retrieve the first annotation on the source page.
        Page sourcePage = doc.Pages[sourcePageNumber];
        if (sourcePage.Annotations.Count == 0)
            throw new InvalidOperationException("Source page contains no annotations to clone.");

        // For demonstration we clone the first annotation.
        Annotation original = sourcePage.Annotations[1];

        // Create a new annotation of the same concrete type.
        // Most annotation types have a constructor that accepts (Page, Rectangle).
        // We'll use reflection to instantiate the same type dynamically.
        Type annotType = original.GetType();
        var ctor = annotType.GetConstructor(new Type[] { typeof(Page), typeof(Rectangle) });
        if (ctor == null)
            throw new NotSupportedException($"Annotation type '{annotType.Name}' does not have a supported constructor.");

        // Clone rectangle to preserve position.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
            original.Rect.LLX, original.Rect.LLY,
            original.Rect.URX, original.Rect.URY);

        // Instantiate the cloned annotation on the target page.
        Page targetPage = doc.Pages[targetPageNumber];
        Annotation cloned = (Annotation)ctor.Invoke(new object[] { targetPage, rect });

        // Copy common properties.
        cloned.Contents = original.Contents;
        cloned.Modified = DateTime.Now;
        cloned.Color = newColor; // Apply the new color.

        // The "Open" property exists only on TextAnnotation (sticky‑note) types.
        // Copy it safely when both original and clone are TextAnnotation instances.
        if (original is TextAnnotation origText && cloned is TextAnnotation cloneText)
        {
            cloneText.Open = origText.Open;
        }

        // Title and Subject exist only on markup annotations – copy them safely.
        if (original is MarkupAnnotation originalMarkup && cloned is MarkupAnnotation clonedMarkup)
        {
            clonedMarkup.Title = originalMarkup.Title;
            clonedMarkup.Subject = originalMarkup.Subject;
        }

        // Add the cloned annotation to the target page.
        targetPage.Annotations.Add(cloned);

        // Save the modified document.
        editor.Save(outputPath);
        editor.Close();
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public static void Main(string[] args)
    {
        // No demonstration code required for the library method.
    }
}

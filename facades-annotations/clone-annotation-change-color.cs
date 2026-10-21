using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class AnnotationHelper
{
    /// <summary>
    /// Clones an annotation from a source page, changes its color, and adds the clone to a destination page.
    /// </summary>
    /// <param name="sourcePdfPath">Path to the PDF containing the original annotation.</param>
    /// <param name="sourcePageNumber">1‑based page number where the original annotation resides.</param>
    /// <param name="annotationIndex">Zero‑based index of the annotation on the source page to clone.</param>
    /// <param name="newColor">The new color to apply to the cloned annotation (Aspose.Pdf.Color).</param>
    /// <param name="destPdfPath">Path to the PDF where the cloned annotation will be added.</param>
    /// <param name="destPageNumber">1‑based page number in the destination PDF where the clone will be placed.</param>
    public static void CloneAnnotationChangeColor(
        string sourcePdfPath,
        int sourcePageNumber,
        int annotationIndex,
        Color newColor,
        string destPdfPath,
        int destPageNumber)
    {
        // Validate input files
        if (!File.Exists(sourcePdfPath))
            throw new FileNotFoundException($"Source PDF not found: {sourcePdfPath}");
        if (!File.Exists(destPdfPath))
            throw new FileNotFoundException($"Destination PDF not found: {destPdfPath}");

        // -----------------------------------------------------------------
        // Step 1: Load source PDF and retrieve the requested annotation
        // -----------------------------------------------------------------
        Document sourceDoc = new Document(sourcePdfPath);
        if (sourcePageNumber < 1 || sourcePageNumber > sourceDoc.Pages.Count)
            throw new ArgumentOutOfRangeException(nameof(sourcePageNumber), "Page number out of range in source PDF.");

        Page srcPage = sourceDoc.Pages[sourcePageNumber];
        if (annotationIndex < 0 || annotationIndex >= srcPage.Annotations.Count)
            throw new ArgumentOutOfRangeException(nameof(annotationIndex), "Invalid annotation index on source page.");

        // Deep clone the annotation
        Annotation originalAnnotation = srcPage.Annotations[annotationIndex];
        Annotation clonedAnnotation = (Annotation)originalAnnotation.Clone();

        // -----------------------------------------------------------------
        // Step 2: Change the color of the cloned annotation
        // -----------------------------------------------------------------
        // Most annotation types expose a Color property. Use reflection as a safety net for types that may not.
        var colorProp = clonedAnnotation.GetType().GetProperty("Color");
        if (colorProp != null && colorProp.CanWrite)
        {
            colorProp.SetValue(clonedAnnotation, newColor);
        }
        else
        {
            // If the specific annotation type does not have a Color property, ignore the change.
            // This keeps the method functional across Aspose.Pdf versions.
        }

        // -----------------------------------------------------------------
        // Step 3: Load destination PDF and add the cloned annotation
        // -----------------------------------------------------------------
        Document destDoc = new Document(destPdfPath);
        if (destPageNumber < 1 || destPageNumber > destDoc.Pages.Count)
            throw new ArgumentOutOfRangeException(nameof(destPageNumber), "Page number out of range in destination PDF.");

        Page dstPage = destDoc.Pages[destPageNumber];
        dstPage.Annotations.Add(clonedAnnotation);

        // Persist changes
        destDoc.Save(destPdfPath);
    }
}

// A minimal entry point so the project compiles as a console application.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library method can be called from external code or unit tests.
    }
}

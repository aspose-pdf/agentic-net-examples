using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public class AnnotationCloner
{
    /// <summary>
    /// Clones the first text annotation from <paramref name="sourcePageNumber"/>,
    /// changes its color to <paramref name="newColor"/>, and adds the cloned
    /// annotation to <paramref name="targetPageNumber"/> of the same document.
    /// The result is saved to <paramref name="outputPath"/>.
    /// </summary>
    public static void CloneAnnotation(
        string inputPath,
        int sourcePageNumber,
        int targetPageNumber,
        Aspose.Pdf.Color newColor,
        string outputPath)
    {
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input file not found: {inputPath}");

        // Initialize the facade and bind the PDF.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Access the underlying Document (read‑only property).
        Document doc = editor.Document;

        // Validate page numbers (Aspose.Pdf uses 1‑based indexing).
        if (sourcePageNumber < 1 || sourcePageNumber > doc.Pages.Count)
            throw new ArgumentOutOfRangeException(nameof(sourcePageNumber));
        if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            throw new ArgumentOutOfRangeException(nameof(targetPageNumber));

        // Extract text annotations from the source page.
        // AnnotationType.Text corresponds to TextAnnotation.
        var sourceAnnotationsList = editor.ExtractAnnotations(
            sourcePageNumber,
            sourcePageNumber,
            new AnnotationType[] { AnnotationType.Text });

        // Convert IList<Annotation> to an array for easier handling.
        Annotation[] sourceAnnotations = sourceAnnotationsList?.ToArray() ?? Array.Empty<Annotation>();

        if (sourceAnnotations.Length == 0)
            throw new InvalidOperationException("No text annotations found on the source page.");

        // Take the first annotation as the example to clone.
        TextAnnotation original = sourceAnnotations[0] as TextAnnotation;
        if (original == null)
            throw new InvalidCastException("Extracted annotation is not a TextAnnotation.");

        // Prepare the target page.
        Page targetPage = doc.Pages[targetPageNumber];

        // Clone by creating a new TextAnnotation with the same rectangle.
        Aspose.Pdf.Rectangle rect = original.Rect;
        TextAnnotation clone = new TextAnnotation(targetPage, rect)
        {
            // Copy desired properties from the original.
            Contents = original.Contents,
            Title    = original.Title,
            // Change the color as requested.
            Color    = newColor,
            // Preserve other visual settings if needed.
            Opacity  = original.Opacity,
            Border   = original.Border,
            // Keep the same open state.
            Open     = original.Open
        };

        // Add the cloned annotation to the target page.
        targetPage.Annotations.Add(clone);

        // Save the modified document.
        editor.Save(outputPath);

        // Release resources.
        editor.Close();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Simple argument handling – in a real scenario you would add validation.
        if (args.Length != 5)
        {
            Console.WriteLine("Usage: <inputPath> <sourcePage> <targetPage> <color> <outputPath>");
            Console.WriteLine("Example color values: Red, Green, Blue");
            return;
        }

        string inputPath = args[0];
        int sourcePage = int.Parse(args[1]);
        int targetPage = int.Parse(args[2]);
        string colorName = args[3];
        string outputPath = args[4];

        // Map simple color names to Aspose.Pdf.Color instances.
        Aspose.Pdf.Color newColor = colorName.ToLower() switch
        {
            "red" => Aspose.Pdf.Color.Red,
            "green" => Aspose.Pdf.Color.Green,
            "blue" => Aspose.Pdf.Color.Blue,
            _ => Aspose.Pdf.Color.Black
        };

        AnnotationCloner.CloneAnnotation(inputPath, sourcePage, targetPage, newColor, outputPath);
        Console.WriteLine($"Cloned annotation saved to {outputPath}");
    }
}

// Example usage (uncomment to run directly without command‑line args):
// AnnotationCloner.CloneAnnotation(
//     inputPath: "sample.pdf",
//     sourcePageNumber: 1,
//     targetPageNumber: 2,
//     newColor: Aspose.Pdf.Color.Red,
//     outputPath: "sample_cloned.pdf");
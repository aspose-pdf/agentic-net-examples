using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

// Minimal stubs for the xUnit testing framework when the package is not referenced.
namespace Xunit
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class FactAttribute : Attribute { }

    public static class Assert
    {
        public static void IsType<T>(object obj)
        {
            if (!(obj is T))
                throw new Exception($"Assert.IsType failed. Expected type {typeof(T)}, actual type {obj?.GetType()}.");
        }

        // Equality with a precision (number of decimal places) similar to xUnit's overload.
        public static void Equal(double expected, double actual, int precision)
        {
            double tolerance = Math.Pow(10, -precision);
            if (Math.Abs(expected - actual) > tolerance)
                throw new Exception($"Assert.Equal failed. Expected {expected} +/- {tolerance}, actual {actual}.");
        }
    }
}

public class TextAnnotationPositionTests
{
    [Xunit.Fact]
    public void TextAnnotation_IsPlacedAtExpectedCoordinates()
    {
        // Define temporary file paths
        string tempPdfPath = Path.ChangeExtension(Path.GetTempFileName(), ".pdf");

        // Expected rectangle coordinates (lower‑left X/Y, upper‑right X/Y)
        double llx = 100, lly = 200, urx = 150, ury = 250;

        // Create a simple PDF with one blank page and add a TextAnnotation
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create the annotation and set basic properties
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Test Note",
                Contents = "This is a test annotation.",
                Open     = true
            };

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the PDF to the temporary location
            doc.Save(tempPdfPath);
        }

        // Load the PDF using a Facade class (PdfAnnotationEditor) and verify the annotation position
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the previously saved PDF
            editor.BindPdf(tempPdfPath);

            // Retrieve the first page (1‑based) and its first annotation
            Page loadedPage = editor.Document.Pages[1];
            Annotation loadedAnn = loadedPage.Annotations[1];

            // Ensure the annotation is a TextAnnotation
            Xunit.Assert.IsType<TextAnnotation>(loadedAnn);
            TextAnnotation loadedTextAnn = (TextAnnotation)loadedAnn;

            // Verify the rectangle coordinates match the expected values
            Aspose.Pdf.Rectangle loadedRect = loadedTextAnn.Rect;
            Xunit.Assert.Equal(llx, loadedRect.LLX, 5);
            Xunit.Assert.Equal(lly, loadedRect.LLY, 5);
            Xunit.Assert.Equal(urx, loadedRect.URX, 5);
            Xunit.Assert.Equal(ury, loadedRect.URY, 5);
        }

        // Clean up the temporary file
        if (File.Exists(tempPdfPath))
        {
            File.Delete(tempPdfPath);
        }
    }
}

// Dummy entry point to satisfy the C# compiler when the project is built as an executable.
public static class Program
{
    public static void Main() { /* No‑op */ }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfIntegrityHelper
{
    /// <summary>
    /// Loads a PDF, adds a simple text annotation, saves the modified file,
    /// then validates the document for corrupted objects.
    /// Validation result is returned and a detailed log is written to <paramref name="logPath"/>.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the modified PDF will be saved.</param>
    /// <param name="logPath">Path to the validation log file.</param>
    /// <returns>True if the document passes validation; otherwise false.</returns>
    public static bool ValidatePdfAfterAnnotation(string inputPath, string outputPath, string logPath)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return false;
        }

        // Load the original PDF.
        using (Document originalDoc = new Document(inputPath))
        {
            // Define a rectangle for the annotation (coordinates are in points).
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Get the page we want to annotate (first page in this example).
            Page page = originalDoc.Pages[1];

            // Create a text annotation. The constructor expects the page first, then the rectangle.
            var textAnnotation = new TextAnnotation(page, rect)
            {
                Title = "AsposeUser",          // author
                Subject = "SampleAnnotation", // subject
                Open = true,                    // open flag
                Icon = TextIcon.Note            // icon type (Note)
            };
            // Set the annotation's visible text.
            textAnnotation.Contents = "This is a test annotation.";

            // Add the annotation to the page.
            page.Annotations.Add(textAnnotation);

            // Save the modified PDF.
            originalDoc.Save(outputPath);
        }

        // Load the newly saved PDF for validation.
        using (Document modifiedDoc = new Document(outputPath))
        {
            bool isValid = false;
            try
            {
                // Validate the document; the method writes a log file and returns true if no errors are found.
                isValid = modifiedDoc.Validate(logPath, PdfFormat.PDF_A_1B);
            }
            catch (ObjectReferenceCorruptedException ex)
            {
                // This exception indicates a corrupted object reference.
                Console.Error.WriteLine($"Corrupted object detected: {ex.Message}");
                isValid = false;
            }
            catch (Exception ex)
            {
                // Any other unexpected errors.
                Console.Error.WriteLine($"Validation failed: {ex.Message}");
                isValid = false;
            }

            return isValid;
        }
    }
}

// A minimal entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // The Main method is intentionally left minimal. It can be used for quick manual testing.
        // Example usage (uncomment and adjust paths as needed):
        // bool result = PdfIntegrityHelper.ValidatePdfAfterAnnotation("input.pdf", "output.pdf", "validation.log");
        // Console.WriteLine($"Validation result: {result}");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // For annotation types

public static class PdfIntegrityHelper
{
    /// <summary>
    /// Validates a PDF after annotation modifications.
    /// Returns true if the document passes validation without corrupted objects.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    /// <param name="logFilePath">Path to the validation log file.</param>
    /// <returns>True if validation succeeds; otherwise false.</returns>
    public static bool ValidatePdfAfterAnnotations(string inputPdfPath, string outputPdfPath, string logFilePath)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return false;
        }

        try
        {
            // Load the source PDF.
            Document doc = new Document(inputPdfPath);

            // --- Example annotation modification ---
            // Create a simple text (markup) annotation on the first page.
            // Rectangle constructor: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            var annotationRect = new Rectangle(100, 500, 300, 600);
            // TextAnnotation derives from MarkupAnnotation and represents a sticky‑note style annotation.
            TextAnnotation txtAnno = new TextAnnotation(doc.Pages[1], annotationRect)
            {
                Contents = "Sample annotation",
                Title = "Note",
                // The Subject property is commonly used to store the author/creator of the annotation.
                Subject = "Author",
                // Open = true makes the annotation popup visible when the document is opened.
                Open = true
            };
            // Add the annotation to the page.
            doc.Pages[1].Annotations.Add(txtAnno);

            // Save the modified PDF.
            doc.Save(outputPdfPath);

            // Validate the resulting document.
            // The Validate method writes a detailed log and returns true if no corrupted objects are found.
            bool isValid = doc.Validate(logFilePath, PdfFormat.PDF_A_1B);
            return isValid;
        }
        catch (ObjectReferenceCorruptedException ex)
        {
            // Specific exception indicating a corrupted object reference.
            Console.Error.WriteLine($"Corrupted object detected: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // General error handling.
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
            return false;
        }
    }

    // Entry point required for a console application.
    public static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath> <logFilePath>");
            return;
        }

        string inputPdf = args[0];
        string outputPdf = args[1];
        string logFile = args[2];

        bool result = ValidatePdfAfterAnnotations(inputPdf, outputPdf, logFile);
        Console.WriteLine($"Validation result: {result}");
    }
}

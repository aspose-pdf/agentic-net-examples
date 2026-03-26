using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class AnnotationHelper
{
    /// <summary>
    /// Returns the list of annotation names whose author (stored in the Subject property) matches the specified author string.
    /// </summary>
    /// <param name="pdfPath">Path to the input PDF file.</param>
    /// <param name="author">Author name to filter annotations.</param>
    /// <returns>List of annotation names.</returns>
    public static List<string> GetAnnotationNamesByAuthor(string pdfPath, string author)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        var matchingNames = new List<string>();

        using (Document doc = new Document(pdfPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Only markup annotations expose the Subject property (used here to store the author)
                    if (annotation is MarkupAnnotation markup &&
                        string.Equals(markup.Subject, author, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(annotation.Name))
                            matchingNames.Add(annotation.Name);
                    }
                }
            }
        }

        return matchingNames;
    }

    // ---------------------------------------------------------------------
    // Helper: creates a minimal PDF with a single text annotation if the
    // specified file does not exist. This allows the sample to run without
    // requiring an external PDF.
    // ---------------------------------------------------------------------
    private static void EnsureSamplePdf(string path, string author)
    {
        if (File.Exists(path))
            return;

        // Create a one‑page document
        var doc = new Document();
        var page = doc.Pages.Add();

        // Add a simple text annotation and store the author in the Subject field
        var textAnnot = new TextAnnotation(page, new Aspose.Pdf.Rectangle(100, 600, 300, 650))
        {
            Name = "SampleAnnotation",
            Subject = author, // author information
            Title = "Demo",
            Contents = "This is a demo annotation."
        };
        page.Annotations.Add(textAnnot);

        doc.Save(path);
    }

    // Example usage
    public static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string targetAuthor = "John Doe";

        // Ensure a PDF exists so the demo does not throw FileNotFoundException
        EnsureSamplePdf(inputPdf, targetAuthor);

        List<string> names;
        try
        {
            names = GetAnnotationNamesByAuthor(inputPdf, targetAuthor);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return;
        }

        Console.WriteLine($"Annotations authored by '{targetAuthor}':");
        if (names.Count == 0)
        {
            Console.WriteLine("  (none found)");
        }
        else
        {
            foreach (string name in names)
            {
                Console.WriteLine($"  {name}");
            }
        }
    }
}

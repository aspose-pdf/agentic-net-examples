using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns true if the PDF contains any annotations whose Hidden flag is set.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file to inspect.</param>
    public static bool HasHiddenAnnotations(string pdfPath)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Load the document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Iterate through all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotation collections also use 1‑based indexing.
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // Check if the Hidden flag is set.
                    if (annotation.Flags.HasFlag(AnnotationFlags.Hidden))
                        return true;
                }
            }

            // No hidden annotations found.
            return false;
        }
    }
}

public class Program
{
    /// <summary>
    /// Simple console entry point used to satisfy the compiler and optionally demonstrate the helper.
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: <executable> <pdf-path>");
            return;
        }

        string pdfPath = args[0];
        try
        {
            bool hasHidden = PdfAnnotationHelper.HasHiddenAnnotations(pdfPath);
            Console.WriteLine(hasHidden ? "Hidden annotations found." : "No hidden annotations.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
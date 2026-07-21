using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfAnnotationUtility
{
    public static class PdfAnnotationHelper
    {
        /// <summary>
        /// Returns true if the PDF contains any annotations whose Hidden flag is set.
        /// </summary>
        /// <param name="pdfPath">Path to the PDF file to inspect.</param>
        /// <returns>True if at least one hidden annotation is present; otherwise, false.</returns>
        public static bool HasHiddenAnnotations(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath) || !File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            // Load the document using the recommended lifecycle pattern (using block).
            using (Document doc = new Document(pdfPath))
            {
                // Iterate through all pages (1‑based indexing).
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Annotations collection also uses 1‑based indexing.
                    for (int j = 1; j <= page.Annotations.Count; j++)
                    {
                        Annotation annotation = page.Annotations[j];

                        // Check if the Hidden flag is set.
                        if ((annotation.Flags & AnnotationFlags.Hidden) == AnnotationFlags.Hidden)
                            return true;
                    }
                }
            }

            // No hidden annotations were found.
            return false;
        }
    }

    // Minimal entry point required for an executable project.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Optional demo: if a PDF path is supplied, report hidden‑annotation status.
            if (args.Length > 0)
            {
                try
                {
                    bool hasHidden = PdfAnnotationHelper.HasHiddenAnnotations(args[0]);
                    Console.WriteLine(hasHidden ? "Hidden annotations found." : "No hidden annotations.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
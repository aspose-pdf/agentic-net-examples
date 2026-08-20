using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfUtilities
{
    public static class PdfAnnotationHelper
    {
        /// <summary>
        /// Returns true if the PDF contains any annotations whose Hidden flag is set.
        /// </summary>
        /// <param name="pdfPath">Path to the PDF file to inspect.</param>
        /// <returns>True if at least one hidden annotation is present; otherwise, false.</returns>
        public static bool ContainsHiddenAnnotations(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            // Load the document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // Iterate over all pages.
                foreach (Page page in doc.Pages)
                {
                    // Iterate over all annotations on the current page.
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Check if the Hidden flag is set.
                        if ((annotation.Flags & AnnotationFlags.Hidden) == AnnotationFlags.Hidden)
                        {
                            return true; // Hidden annotation found.
                        }
                    }
                }

                // No hidden annotations were encountered.
                return false;
            }
        }
    }

    // Simple entry point required by the build system.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // If a PDF path is supplied, report whether hidden annotations exist.
            if (args.Length > 0)
            {
                string pdfPath = args[0];
                try
                {
                    bool hasHidden = PdfAnnotationHelper.ContainsHiddenAnnotations(pdfPath);
                    Console.WriteLine(hasHidden
                        ? "Hidden annotations were found in the PDF."
                        : "No hidden annotations were found in the PDF.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: PdfUtilities <pdf-file-path>");
            }
        }
    }
}

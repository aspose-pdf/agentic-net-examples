using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfAnnotationUtility
{
    public static class PdfAnnotationHelper
    {
        /// <summary>
        /// Returns true if the PDF contains any annotations that are marked as hidden
        /// (AnnotationFlags.Hidden or AnnotationFlags.NoView) after the document is loaded.
        /// </summary>
        /// <param name="pdfPath">Path to the PDF file to inspect.</param>
        /// <returns>True if at least one hidden annotation is found; otherwise, false.</returns>
        public static bool HasHiddenAnnotations(string pdfPath)
        {
            // Load the PDF document (using the required load lifecycle rule)
            using (Document doc = new Document(pdfPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing internally)
                foreach (Page page in doc.Pages)
                {
                    // Iterate through the annotation collection of the current page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Check the Flags property for Hidden or NoView bits
                        if ((annotation.Flags & AnnotationFlags.Hidden) == AnnotationFlags.Hidden ||
                            (annotation.Flags & AnnotationFlags.NoView) == AnnotationFlags.NoView)
                        {
                            // A hidden annotation was found
                            return true;
                        }
                    }
                }

                // No hidden annotations detected
                return false;
            }
        }
    }

    internal class Program
    {
        // Required entry point for a console‑style project.
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: PdfAnnotationUtility <pdf-path>");
                return;
            }

            string pdfPath = args[0];
            bool hasHidden = PdfAnnotationHelper.HasHiddenAnnotations(pdfPath);
            Console.WriteLine(hasHidden ? "Hidden annotations were found." : "No hidden annotations were found.");
        }
    }
}
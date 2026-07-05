using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class AnnotationHelper
    {
        /// <summary>
        /// Clones the first annotation on the specified page, modifies some of its properties,
        /// and adds the cloned annotation back to the same page.
        /// The PDF is processed using Aspose.Pdf.Facades (PdfAnnotationEditor).
        /// </summary>
        /// <param name="inputPdf">Path to the source PDF file.</param>
        /// <param name="outputPdf">Path where the modified PDF will be saved.</param>
        /// <param name="pageNumber">1‑based page number to work on.</param>
        public static void CloneModifyAndAddAnnotation(string inputPdf, string outputPdf, int pageNumber)
        {
            if (!File.Exists(inputPdf))
                throw new FileNotFoundException($"Input file not found: {inputPdf}");

            // Load the document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdf))
            {
                // Bind the document to the PdfAnnotationEditor facade.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
                {
                    // Validate page number (Aspose.Pdf uses 1‑based indexing).
                    if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                        throw new ArgumentOutOfRangeException(nameof(pageNumber), "Invalid page number.");

                    // Get the target page.
                    Page page = doc.Pages[pageNumber];

                    // Ensure the page contains at least one annotation.
                    if (page.Annotations == null || page.Annotations.Count == 0)
                        throw new InvalidOperationException($"Page {pageNumber} does not contain any annotations.");

                    // Retrieve the first annotation on the page (1‑based collection index).
                    Annotation original = page.Annotations[1];

                    // Currently we support cloning of TextAnnotation. Extend as needed.
                    if (original is TextAnnotation textAnnot)
                    {
                        // Clone by creating a new instance with the same rectangle and copying properties.
                        TextAnnotation cloned = new TextAnnotation(page, textAnnot.Rect)
                        {
                            Title = textAnnot.Title,
                            Contents = textAnnot.Contents,
                            Color = textAnnot.Color,
                            Modified = textAnnot.Modified,
                            Subject = textAnnot.Subject,
                            Open = textAnnot.Open
                        };

                        // Modify desired properties.
                        cloned.Title = $"{cloned.Title} (Clone)";
                        cloned.Contents = $"{cloned.Contents} – modified";
                        cloned.Color = Aspose.Pdf.Color.Red; // example modification

                        // Add the cloned annotation back to the same page.
                        page.Annotations.Add(cloned);
                    }
                    else
                    {
                        throw new NotSupportedException($"Cloning for annotation type '{original.GetType().Name}' is not implemented.");
                    }

                    // Save the modified document using the facade's Save method.
                    editor.Save(outputPdf);
                }
            }
        }
    }

    internal class Program
    {
        // Simple entry point required for a console‑application build.
        private static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: AsposePdfApi <inputPdf> <outputPdf> <pageNumber>");
                return;
            }

            string inputPdf = args[0];
            string outputPdf = args[1];
            if (!int.TryParse(args[2], out int pageNumber))
            {
                Console.WriteLine("Invalid page number.");
                return;
            }

            try
            {
                AnnotationHelper.CloneModifyAndAddAnnotation(inputPdf, outputPdf, pageNumber);
                Console.WriteLine("Annotation cloned and saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

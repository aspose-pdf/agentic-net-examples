using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace AsposePdfAnnotationWrapper
{
    // Wrapper that abstracts PdfAnnotationEditor and provides common annotation operations.
    public class PdfAnnotationEditorWrapper : IDisposable
    {
        private readonly Document _document;
        private readonly PdfAnnotationEditor _editor;

        // Load PDF and bind the editor.
        public PdfAnnotationEditorWrapper(string pdfPath)
        {
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF not found: {pdfPath}");

            // Core Document used for adding/retrieving annotations.
            _document = new Document(pdfPath);

            // Facade editor used for bulk operations (delete, flatten, save).
            _editor = new PdfAnnotationEditor();
            _editor.BindPdf(pdfPath);
        }

        // Add a TextAnnotation to a specific page.
        public void AddTextAnnotation(int pageNumber, Aspose.Pdf.Rectangle rect, string title, string contents)
        {
            // Aspose.Pdf uses 1‑based page indexing.
            Page page = _document.Pages[pageNumber];
            var textAnno = new TextAnnotation(page, rect)
            {
                Title    = title,
                Contents = contents,
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(textAnno);
        }

        // Delete all annotations in the document.
        public void DeleteAllAnnotations()
        {
            _editor.DeleteAnnotations();
        }

        // Delete a single annotation on a page (if it exists).
        public void DeleteAnnotation(int pageNumber, int annotationIndex)
        {
            // Use the Document API because PdfAnnotationEditor does not expose a 2‑parameter overload.
            Page page = _document.Pages[pageNumber];
            if (annotationIndex >= 0 && annotationIndex < page.Annotations.Count)
            {
                // AnnotationCollection uses zero‑based indexing for Delete().
                page.Annotations.Delete(annotationIndex);
            }
        }

        // Retrieve the collection of annotations on a given page.
        public AnnotationCollection GetAnnotations(int pageNumber)
        {
            return _document.Pages[pageNumber].Annotations;
        }

        // Flatten remaining annotations into the page content.
        public void FlattenAnnotations()
        {
            // Correct method name per Aspose.Pdf documentation.
            _editor.FlatteningAnnotations();
        }

        // Save the modified PDF.
        public void Save(string outputPath)
        {
            // Save via the core Document to include any annotations added through it.
            _document.Save(outputPath);
        }

        // Dispose the underlying Document. PdfAnnotationEditor does not implement IDisposable.
        public void Dispose()
        {
            _document?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdf  = "input.pdf";
            const string outputPdf = "annotated_output.pdf";

            // Ensure the input file exists.
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"File not found: {inputPdf}");
                return;
            }

            // Use the wrapper inside a using block for deterministic disposal.
            using (var editor = new PdfAnnotationEditorWrapper(inputPdf))
            {
                // Example: add a text annotation on the first page.
                var rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
                editor.AddTextAnnotation(1, rect, "Sample Note", "This is an accessible annotation.");

                // Delete all existing annotations (if any).
                editor.DeleteAllAnnotations();

                // Flatten any remaining annotations into the page content.
                editor.FlattenAnnotations();

                // Save the result.
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Annotated PDF saved to '{outputPdf}'.");
        }
    }
}
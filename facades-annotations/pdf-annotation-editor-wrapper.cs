using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace PdfAnnotationWrapperDemo
{
    // Wrapper class that abstracts PdfAnnotationEditor and provides common annotation operations
    public class PdfAnnotationHelper : IDisposable
    {
        private PdfAnnotationEditor _editor;
        private Document _document;

        // Loads a PDF file and binds it to the internal PdfAnnotationEditor
        public void Load(string pdfPath)
        {
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            // Load the document for direct manipulation (e.g., adding annotations)
            _document = new Document(pdfPath);

            // Bind the same file to the annotation editor for delete/flatten operations
            _editor = new PdfAnnotationEditor();
            _editor.BindPdf(pdfPath);
        }

        // Adds a text annotation to the specified page
        public void AddTextAnnotation(int pageNumber, Aspose.Pdf.Rectangle rect, string title, string contents)
        {
            // Aspose.Pdf uses 1‑based page indexing
            if (_document == null)
                throw new InvalidOperationException("Document not loaded. Call Load() first.");

            if (pageNumber < 1 || pageNumber > _document.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number out of range.");

            // Retrieve the target page
            Page page = _document.Pages[pageNumber];

            // Create the annotation and set its properties
            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Title    = title;
            annotation.Contents = contents;
            annotation.Open     = true;
            annotation.Icon     = TextIcon.Note;

            // Add the annotation to the page
            page.Annotations.Add(annotation);
        }

        // Deletes all annotations from the bound PDF
        public void DeleteAllAnnotations()
        {
            if (_editor == null)
                throw new InvalidOperationException("Editor not initialized. Call Load() first.");

            _editor.DeleteAnnotations();
        }

        // Flattens remaining annotations (makes them part of the page content)
        public void FlattenAnnotations()
        {
            if (_editor == null)
                throw new InvalidOperationException("Editor not initialized. Call Load() first.");

            // Correct method name is FlatteningAnnotations()
            _editor.FlatteningAnnotations();
        }

        // Saves the modified PDF to the specified output path
        public void Save(string outputPath)
        {
            if (_document == null)
                throw new InvalidOperationException("Document not loaded. Call Load() first.");

            // First, save any changes made directly on the Document (e.g., added annotations)
            _document.Save(outputPath);

            // If delete/flatten operations were performed via the editor, re‑bind and save again
            if (_editor != null)
            {
                _editor.BindPdf(outputPath);
                _editor.Save(outputPath);
            }
        }

        // Dispose pattern – Document implements IDisposable; PdfAnnotationEditor does not
        public void Dispose()
        {
            _document?.Dispose();
            // No need to dispose _editor
        }
    }

    // Simple console program demonstrating the wrapper usage
    class Program
    {
        static void Main(string[] args)
        {
            const string inputPdf  = "sample.pdf";
            const string outputPdf = "sample_annotated.pdf";

            // Define a rectangle for the annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            try
            {
                using (PdfAnnotationHelper helper = new PdfAnnotationHelper())
                {
                    // Load the source PDF
                    helper.Load(inputPdf);

                    // Add a text annotation on page 1
                    helper.AddTextAnnotation(1, rect, "Note Title", "This is an example annotation.");

                    // Optionally delete all existing annotations
                    // helper.DeleteAllAnnotations();

                    // Optionally flatten annotations into the page content
                    // helper.FlattenAnnotations();

                    // Save the result
                    helper.Save(outputPdf);
                }

                Console.WriteLine($"Annotated PDF saved to '{outputPdf}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
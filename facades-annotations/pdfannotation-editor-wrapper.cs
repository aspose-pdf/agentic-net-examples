using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfAnnotationHelper
{
    // Abstract wrapper exposing simplified annotation operations
    public abstract class PdfAnnotationEditorWrapper : IDisposable
    {
        protected PdfAnnotationEditor editor;
        protected Document document;

        protected PdfAnnotationEditorWrapper(Document doc)
        {
            this.document = doc;
            this.editor = new PdfAnnotationEditor(this.document);
        }

        // Deletes a specific annotation by its name
        public void DeleteAnnotation(string annotationName)
        {
            this.editor.DeleteAnnotation(annotationName);
        }

        // Deletes all annotations in the document
        public void DeleteAllAnnotations()
        {
            this.editor.DeleteAnnotations();
        }

        // Deletes all annotations of a specific type (e.g., "Text")
        public void DeleteAnnotationsByType(string annotationType)
        {
            this.editor.DeleteAnnotations(annotationType);
        }

        // Flattens all annotations (makes them part of the page content)
        public void FlattenAllAnnotations()
        {
            this.editor.FlatteningAnnotations();
        }

        // Exports all annotations to an XFDF file
        public void ExportAnnotations(string xfdfFilePath)
        {
            using (FileStream fs = new FileStream(xfdfFilePath, FileMode.Create, FileAccess.Write))
            {
                this.editor.ExportAnnotationsToXfdf(fs);
            }
        }

        // Saves the modified PDF to a new file
        public void Save(string outputFilePath)
        {
            this.editor.Save(outputFilePath);
        }

        // Releases resources held by the facade and the underlying document
        public void Dispose()
        {
            if (this.editor != null)
            {
                this.editor.Close();
                this.editor = null;
            }
            if (this.document != null)
            {
                this.document.Dispose();
                this.document = null;
            }
        }
    }

    // Concrete implementation used by client code
    public sealed class SimplePdfAnnotationEditor : PdfAnnotationEditorWrapper
    {
        public SimplePdfAnnotationEditor(string pdfPath) : base(new Document(pdfPath))
        {
        }
    }

    // Sample usage
    public class Program
    {
        public static void Main()
        {
            const string inputPdf = "input.pdf";
            const string outputPdf = "output.pdf";
            const string xfdfPath = "annotations.xfdf";

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"File not found: {inputPdf}");
                return;
            }

            using (SimplePdfAnnotationEditor editor = new SimplePdfAnnotationEditor(inputPdf))
            {
                // Remove all existing annotations
                editor.DeleteAllAnnotations();

                // Flatten any remaining annotations (if any)
                editor.FlattenAllAnnotations();

                // Export the (now empty) annotation set to XFDF for demonstration
                editor.ExportAnnotations(xfdfPath);

                // Save the resulting PDF
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Processed PDF saved as '{outputPdf}'. XFDF exported to '{xfdfPath}'.");
        }
    }
}

using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace PdfAnnotationUtilities
{
    /// <summary>
    /// Simplified wrapper around Aspose.Pdf.Facades.PdfAnnotationEditor
    /// providing common annotation operations.
    /// </summary>
    public class PdfAnnotationHelper
    {
        /// <summary>
        /// Deletes all annotations from the specified PDF and saves the result.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the cleaned PDF will be saved.</param>
        public void DeleteAllAnnotations(string inputPdfPath, string outputPdfPath)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.DeleteAnnotations();               // remove every annotation
                editor.Save(outputPdfPath);                // persist changes
            }
        }

        /// <summary>
        /// Flattens all annotations in the PDF (makes them part of the page content).
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the flattened PDF will be saved.</param>
        public void FlattenAllAnnotations(string inputPdfPath, string outputPdfPath)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.FlatteningAnnotations();           // flatten every annotation
                editor.Save(outputPdfPath);
            }
        }

        /// <summary>
        /// Exports all annotations from a PDF to an XFDF stream.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="xfdfStream">Stream that will receive the XFDF data.</param>
        public void ExportAnnotations(string inputPdfPath, Stream xfdfStream)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        /// <summary>
        /// Imports annotations from an FDF file into a PDF and saves the result.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="fdfFilePath">Path to the FDF file containing annotations.</param>
        /// <param name="outputPdfPath">Path where the updated PDF will be saved.</param>
        public void ImportAnnotationsFromFdf(string inputPdfPath, string fdfFilePath, string outputPdfPath)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.ImportAnnotationsFromFdf(fdfFilePath);
                editor.Save(outputPdfPath);
            }
        }

        /// <summary>
        /// Redacts a rectangular area on a specific page, removing its content.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the redacted PDF will be saved.</param>
        /// <param name="pageNumber">1‑based page index where the redaction will be applied.</param>
        /// <param name="rect">Rectangle defining the area to redact.</param>
        /// <param name="color">Color used for the redaction overlay (System.Drawing.Color).</param>
        public void RedactArea(string inputPdfPath, string outputPdfPath, int pageNumber, Aspose.Pdf.Rectangle rect, System.Drawing.Color color)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.RedactArea(pageNumber, rect, color);
                editor.Save(outputPdfPath);
            }
        }

        /// <summary>
        /// Deletes all annotations of a specific type from a PDF.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
        /// <param name="annotationType">The type of annotation to delete (e.g., AnnotationType.Text, AnnotationType.Highlight).</param>
        public void DeleteAnnotationsByType(string inputPdfPath, string outputPdfPath, AnnotationType annotationType)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                // DeleteAnnotations expects a single string representing the annotation type name.
                string typeName = annotationType.ToString();
                editor.DeleteAnnotations(typeName);
                editor.Save(outputPdfPath);
            }
        }

        /// <summary>
        /// Modifies the author of annotations within a page range.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the updated PDF will be saved.</param>
        /// <param name="startPage">1‑based start page.</param>
        /// <param name="endPage">1‑based end page.</param>
        /// <param name="oldAuthor">Existing author name to replace.</param>
        /// <param name="newAuthor">New author name.</param>
        public void ModifyAnnotationAuthor(string inputPdfPath, string outputPdfPath, int startPage, int endPage, string oldAuthor, string newAuthor)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.ModifyAnnotationsAuthor(startPage, endPage, oldAuthor, newAuthor);
                editor.Save(outputPdfPath);
            }
        }
    }

    // Minimal entry point to satisfy the compiler when the project is built as an executable.
    // The helper class is intended for library use; the Main method simply demonstrates a no‑op.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library can be used by referencing the PdfAnnotationHelper class.
        }
    }
}

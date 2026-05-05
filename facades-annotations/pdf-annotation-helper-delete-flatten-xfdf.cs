using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // Required for AnnotationType

namespace AsposePdfApi
{
    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // The helper class can still be used from other projects or unit‑tests.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – the library is intended to be used via its static helper methods.
        }
    }

    public static class PdfAnnotationHelper
    {
        // Binds a PDF file, deletes all annotations, and saves the result.
        public static void DeleteAllAnnotations(string inputPdfPath, string outputPdfPath)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);          // bind source PDF
                editor.DeleteAnnotations();            // remove every annotation
                editor.Save(outputPdfPath);            // persist changes
            }
        }

        // Binds a PDF file, deletes annotations of a specific type, and saves the result.
        // annotType examples: "Text", "Link", "FreeText", etc.
        public static void DeleteAnnotationsByType(string inputPdfPath, string outputPdfPath, string annotType)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.DeleteAnnotations(annotType);   // remove only the specified type
                editor.Save(outputPdfPath);
            }
        }

        // Binds a PDF file, flattens all annotations, and saves the result.
        public static void FlattenAllAnnotations(string inputPdfPath, string outputPdfPath)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.FlatteningAnnotations();        // flatten every annotation
                editor.Save(outputPdfPath);
            }
        }

        // Binds a PDF file, flattens annotations of given types within a page range, and saves.
        // startPage and endPage are 1‑based (Aspose.Pdf uses 1‑based indexing).
        public static void FlattenAnnotationsRange(
            string inputPdfPath,
            string outputPdfPath,
            int startPage,
            int endPage,
            AnnotationType[] annotationTypes)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.FlatteningAnnotations(startPage, endPage, annotationTypes);
                editor.Save(outputPdfPath);
            }
        }

        // Exports all annotations of the bound PDF to an XFDF stream.
        public static void ExportAnnotationsToXfdf(string inputPdfPath, Stream xfdfOutputStream)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.ExportAnnotationsToXfdf(xfdfOutputStream);
            }
        }

        // Imports annotations from an XFDF stream (optionally filtered by types) and saves the PDF.
        public static void ImportAnnotationsFromXfdf(
            string inputPdfPath,
            string outputPdfPath,
            Stream xfdfInputStream,
            AnnotationType[] annotationTypes)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);
                editor.ImportAnnotationFromXfdf(xfdfInputStream, annotationTypes);
                editor.Save(outputPdfPath);
            }
        }
    }
}

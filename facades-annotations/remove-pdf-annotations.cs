using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public class PdfService
    {
        public Stream RemoveAllAnnotations(Stream pdfInput)
        {
            if (pdfInput == null)
                throw new ArgumentNullException(nameof(pdfInput));

            var cleanedPdf = new MemoryStream();

            using (var annotationEditor = new PdfAnnotationEditor())
            {
                annotationEditor.BindPdf(pdfInput);
                annotationEditor.DeleteAnnotations();
                annotationEditor.Save(cleanedPdf);
            }

            cleanedPdf.Position = 0;
            return cleanedPdf;
        }
    }

    // Minimal entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Placeholder – no operation required.
            // Example usage (optional, can be removed in production):
            // var service = new PdfService();
            // using var input = File.OpenRead("input.pdf");
            // using var output = service.RemoveAllAnnotations(input);
            // File.WriteAllBytes("output.pdf", ((MemoryStream)output).ToArray());
        }
    }
}
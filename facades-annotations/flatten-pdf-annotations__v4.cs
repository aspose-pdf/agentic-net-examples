using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public class PdfAnnotationFlattener
    {
        public static MemoryStream FlattenAnnotations(Stream inputPdfStream)
        {
            if (inputPdfStream == null)
                throw new ArgumentNullException(nameof(inputPdfStream));

            var outputStream = new MemoryStream();
            using (var editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfStream);
                editor.FlatteningAnnotations();
                editor.Save(outputStream);
            }
            outputStream.Position = 0;
            return outputStream;
        }
    }

    // Entry point required when the project is built as an executable.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Placeholder – no runtime logic required for the library.
            // Example usage (commented out):
            // using var input = File.OpenRead("input.pdf");
            // var result = PdfAnnotationFlattener.FlattenAnnotations(input);
            // File.WriteAllBytes("output.pdf", result.ToArray());
        }
    }
}
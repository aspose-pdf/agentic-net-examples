using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfAnnotationCleaner
{
    public static class PdfProcessor
    {
        /// <summary>
        /// Removes all annotations from a PDF supplied as a stream.
        /// The returned MemoryStream contains the cleaned PDF and is positioned at the beginning.
        /// </summary>
        /// <param name="pdfInput">Stream that holds the original PDF.</param>
        /// <returns>A MemoryStream with the annotation‑free PDF.</returns>
        public static MemoryStream RemoveAnnotations(Stream pdfInput)
        {
            if (pdfInput == null) throw new ArgumentNullException(nameof(pdfInput));

            // Ensure the input stream is ready for reading.
            if (pdfInput.CanSeek)
                pdfInput.Position = 0;

            // Bind the PDF to the annotation editor and delete all annotations.
            using var editor = new PdfAnnotationEditor();
            editor.BindPdf(pdfInput);
            editor.DeleteAnnotations();

            // Save the cleaned document into a new memory stream.
            var output = new MemoryStream();
            editor.Save(output);
            output.Position = 0; // reset for the caller
            return output;
        }
    }

    // Simple console demonstration – not an ASP.NET endpoint.
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: PdfAnnotationCleaner <input.pdf> <output.pdf>");
                return;
            }

            var inputPath = args[0];
            var outputPath = args[1];

            using var inputStream = File.OpenRead(inputPath);
            using var cleanedStream = PdfProcessor.RemoveAnnotations(inputStream);
            using var outputStream = File.Create(outputPath);
            cleanedStream.CopyTo(outputStream);

            Console.WriteLine("Annotations removed successfully.");
        }
    }
}
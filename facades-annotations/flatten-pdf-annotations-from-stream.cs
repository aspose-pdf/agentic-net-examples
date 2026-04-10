using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfUtilities
{
    public static class PdfAnnotationFlattener
    {
        /// <summary>
        /// Flattens all annotations in a PDF provided as a stream and returns a new stream containing the modified PDF.
        /// No file system access is performed.
        /// </summary>
        /// <param name="inputPdf">Stream containing the source PDF. The stream must be readable and seekable.</param>
        /// <returns>A MemoryStream with the flattened PDF. Caller is responsible for disposing the returned stream.</returns>
        public static Stream FlattenAnnotations(Stream inputPdf)
        {
            if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));
            if (!inputPdf.CanRead) throw new ArgumentException("Input stream must be readable.", nameof(inputPdf));
            if (!inputPdf.CanSeek) throw new ArgumentException("Input stream must be seekable.", nameof(inputPdf));

            // Ensure the input stream is positioned at the beginning.
            inputPdf.Position = 0;

            // Output stream that will hold the flattened PDF.
            var outputPdf = new MemoryStream();

            // PdfAnnotationEditor implements IDisposable, so use a using block for deterministic disposal.
            using (var editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document from the input stream.
                editor.BindPdf(inputPdf);

                // Flatten all annotations in the document.
                editor.FlatteningAnnotations();

                // Save the modified document to the output stream.
                editor.Save(outputPdf);
            }

            // Reset the position of the output stream so it can be read from the beginning.
            outputPdf.Position = 0;
            return outputPdf;
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library functionality is accessed via PdfAnnotationFlattener.
        }
    }
}

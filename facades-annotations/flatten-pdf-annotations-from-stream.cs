using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfAnnotationFlattener
    {
        /// <summary>
        /// Flattens all annotations in a PDF provided as a stream and returns a new stream containing the modified PDF.
        /// </summary>
        /// <param name="pdfInput">Input stream with the original PDF. The stream will be read from its current position.</param>
        /// <returns>A MemoryStream containing the PDF with flattened annotations. Caller is responsible for disposing the returned stream.</returns>
        public static Stream FlattenPdfAnnotations(Stream pdfInput)
        {
            if (pdfInput == null) throw new ArgumentNullException(nameof(pdfInput));

            // Ensure the input stream is positioned at the beginning.
            if (pdfInput.CanSeek)
                pdfInput.Position = 0;

            // Output stream that will hold the flattened PDF.
            MemoryStream outputStream = new MemoryStream();

            // Use PdfAnnotationEditor from Aspose.Pdf.Facades to work with annotations.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the input PDF stream to the editor.
                editor.BindPdf(pdfInput);

                // Flatten all annotations in the document.
                editor.FlatteningAnnotations();

                // Save the modified PDF to the output stream.
                editor.Save(outputStream);
            }

            // Reset the output stream position so it can be read from the beginning.
            if (outputStream.CanSeek)
                outputStream.Position = 0;

            return outputStream;
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library functionality is exposed via PdfAnnotationFlattener.
        }
    }
}

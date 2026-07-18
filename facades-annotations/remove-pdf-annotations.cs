using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace PdfProcessing
{
    /// <summary>
    /// Provides PDF‑cleaning utilities that work in any .NET host (console, worker service, Azure Function, etc.).
    /// The implementation avoids ASP.NET Core types so it compiles in projects that do not reference Microsoft.AspNetCore.
    /// </summary>
    public static class PdfCleaner
    {
        /// <summary>
        /// Removes all annotations from the PDF supplied via <paramref name="input"/> and returns a new stream
        /// containing the cleaned document.
        /// </summary>
        /// <param name="input">A readable <see cref="Stream"/> that holds a PDF. The stream is not closed by this method.</param>
        /// <returns>A <see cref="MemoryStream"/> positioned at the beginning, ready to be sent back to a client.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="input"/> is null.</exception>
        public static async Task<MemoryStream> CleanPdfAsync(Stream input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            // Ensure we can read from the start of the stream.
            // If the incoming stream is not seek‑able we copy it into a MemoryStream first.
            if (input.CanSeek)
            {
                input.Position = 0;
            }
            else
            {
                input = await CopyToMemoryAsync(input).ConfigureAwait(false);
            }

            var output = new MemoryStream();

            // PdfAnnotationEditor lives in Aspose.Pdf.Facades and is the recommended way to delete annotations.
            using (var editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(input);          // load PDF from the input stream
                editor.DeleteAnnotations();     // remove every annotation
                editor.Save(output);            // write the cleaned PDF to the output stream
            }

            // Reset the output stream so callers can read from the beginning.
            output.Position = 0;
            return output;
        }

        private static async Task<MemoryStream> CopyToMemoryAsync(Stream source)
        {
            var ms = new MemoryStream();
            await source.CopyToAsync(ms).ConfigureAwait(false);
            ms.Position = 0;
            return ms;
        }
    }

    // ---------------------------------------------------------------------
    // Entry point required for a console‑style project (CS5001 fix).
    // The method does nothing functional; it merely satisfies the compiler.
    // ---------------------------------------------------------------------
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            // Placeholder – no operation needed for the library functionality.
            await Task.CompletedTask;
        }
    }
}

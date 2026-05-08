using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public static class PdfFormHelper
    {
        /// <summary>
        /// Loads a PDF from a byte array, fills the specified form fields, and returns the updated PDF as a byte array.
        /// </summary>
        /// <param name="pdfBytes">The original PDF content.</param>
        /// <param name="fieldValues">Dictionary of fully‑qualified field names and the values to set.</param>
        /// <returns>Byte array containing the modified PDF.</returns>
        public static byte[] FillFormFields(byte[] pdfBytes, Dictionary<string, string> fieldValues)
        {
            // Wrap the source PDF bytes in a MemoryStream
            using var inputStream = new MemoryStream(pdfBytes);
            // Form facade for AcroForm manipulation
            using var form = new Form();
            // Bind the PDF stream to the Form facade
            form.BindPdf(inputStream);

            // Fill each field with the provided value
            foreach (var kvp in fieldValues)
            {
                // FillField works with full field names (case‑sensitive)
                form.FillField(kvp.Key, kvp.Value);
            }

            // Capture the modified PDF in a new MemoryStream
            using var outputStream = new MemoryStream();
            form.Save(outputStream);
            // Return the resulting byte array
            return outputStream.ToArray();
        }
    }

    // Dummy entry point required for a console‑application project.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Intentionally left empty – the library functionality is accessed via PdfFormHelper.
        }
    }
}
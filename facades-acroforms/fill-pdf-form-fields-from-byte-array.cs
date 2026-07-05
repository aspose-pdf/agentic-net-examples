using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfFormHelper
    {
        /// <summary>
        /// Loads a PDF from a byte array, fills the specified form fields, and returns the updated PDF as a byte array.
        /// </summary>
        /// <param name="pdfBytes">The original PDF content.</param>
        /// <param name="fieldValues">A dictionary where the key is the full field name and the value is the data to fill.</param>
        /// <returns>Byte array containing the updated PDF.</returns>
        public static byte[] FillPdfForm(byte[] pdfBytes, Dictionary<string, string> fieldValues)
        {
            // Load the PDF from the input byte array using a MemoryStream.
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            // Initialize the Form facade with the stream.
            using (Form form = new Form(inputStream))
            {
                // Fill each field with the provided value.
                foreach (KeyValuePair<string, string> kvp in fieldValues)
                {
                    // FillField(string fieldName, string value) works for text fields, combo boxes, etc.
                    form.FillField(kvp.Key, kvp.Value);
                }

                // Save the modified PDF into an output MemoryStream.
                using (MemoryStream outputStream = new MemoryStream())
                {
                    form.Save(outputStream);
                    // Return the resulting byte array.
                    return outputStream.ToArray();
                }
            }
        }
    }

    // Dummy entry point to satisfy the console‑application requirement.
    // The method does not perform any work; it simply allows the project to compile.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage (optional, can be removed in production):
            // byte[] originalPdf = File.ReadAllBytes("input.pdf");
            // var values = new Dictionary<string, string> { { "Name", "John Doe" } };
            // byte[] updatedPdf = PdfFormHelper.FillPdfForm(originalPdf, values);
            // File.WriteAllBytes("output.pdf", updatedPdf);
        }
    }
}

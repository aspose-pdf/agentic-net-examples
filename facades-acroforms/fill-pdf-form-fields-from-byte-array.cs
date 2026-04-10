using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfFormHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, fills the specified form fields, and returns the updated PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">The original PDF content.</param>
    /// <param name="fieldValues">Dictionary of fully qualified field names and the values to set.</param>
    /// <returns>Byte array containing the modified PDF.</returns>
    public static byte[] FillPdfForm(byte[] pdfBytes, Dictionary<string, string> fieldValues)
    {
        // Load the source PDF from the provided byte array using a MemoryStream.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            // Initialize the Form facade with the input stream.
            using (Form form = new Form(inputStream))
            {
                // Fill each form field with the corresponding value.
                foreach (KeyValuePair<string, string> kvp in fieldValues)
                {
                    // Form.FillField expects the full field name.
                    form.FillField(kvp.Key, kvp.Value);
                }

                // Save the modified PDF into an output MemoryStream.
                using (MemoryStream outputStream = new MemoryStream())
                {
                    form.Save(outputStream);
                    // Return the resulting PDF bytes.
                    return outputStream.ToArray();
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is accessed via PdfFormHelper.
    }
}
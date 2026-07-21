using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfFormHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, fills the specified form fields, and returns the updated PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">The original PDF content.</param>
    /// <param name="fieldValues">A dictionary where the key is the full field name and the value is the text to set.</param>
    /// <returns>Byte array containing the modified PDF.</returns>
    public static byte[] FillPdfForm(byte[] pdfBytes, Dictionary<string, string> fieldValues)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));
        if (fieldValues == null) throw new ArgumentNullException(nameof(fieldValues));

        // Load the PDF from the input byte array using a MemoryStream.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        // Initialize the Form facade with the stream.
        using (Form form = new Form(inputStream))
        {
            // Fill each field with the provided value.
            foreach (var kvp in fieldValues)
            {
                // Use the FillField method that accepts a string value.
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

// Dummy entry point to satisfy the executable project configuration.
public class Program
{
    public static void Main()
    {
        // No operation – the library methods are intended to be called from other code.
    }
}
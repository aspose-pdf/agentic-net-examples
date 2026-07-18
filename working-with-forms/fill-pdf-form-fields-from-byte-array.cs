using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFormFiller
{
    /// <summary>
    /// Loads a PDF from a byte array, fills the specified form fields, and returns the updated PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">The original PDF content.</param>
    /// <param name="fieldValues">Dictionary of field names and the values to set.</param>
    /// <returns>Byte array containing the filled PDF.</returns>
    public static byte[] FillFields(byte[] pdfBytes, Dictionary<string, string> fieldValues)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));
        if (fieldValues == null) throw new ArgumentNullException(nameof(fieldValues));

        // Load the PDF from the input byte array using a MemoryStream.
        using (var inputStream = new MemoryStream(pdfBytes))
        // Create the Document instance; the using block ensures proper disposal.
        using (var doc = new Document(inputStream))
        {
            // Iterate over the supplied field values and assign them to the form fields.
            foreach (var kvp in fieldValues)
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field.
                Field field = doc.Form[kvp.Key] as Field;
                if (field != null)
                {
                    field.Value = kvp.Value;
                }
                // Missing or non‑field annotations are simply ignored.
            }

            // Save the modified document into a new MemoryStream.
            using (var outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                // Return the resulting byte array.
                return outputStream.ToArray();
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as a console application.
public class Program
{
    public static void Main()
    {
        // Intentionally left blank.
    }
}
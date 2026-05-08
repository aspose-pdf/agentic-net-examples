using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFormFiller
{
    /// <summary>
    /// Loads a PDF from a byte array, fills its form fields with the supplied values,
    /// and returns the resulting PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">The source PDF data.</param>
    /// <param name="fieldValues">Dictionary of field names and the values to set.</param>
    /// <returns>Byte array containing the filled PDF.</returns>
    public static byte[] FillFields(byte[] pdfBytes, Dictionary<string, string> fieldValues)
    {
        // Load the PDF from the input memory stream.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Document doc = new Document(inputStream))
        {
            // Iterate over the supplied field values and assign them to the form.
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // The Form indexer returns a generic Field object. Cast it to Field to access the Value property.
                Field field = doc.Form[kvp.Key] as Field;
                if (field != null)
                {
                    field.Value = kvp.Value;
                }
            }

            // Save the modified document into an output memory stream.
            using (MemoryStream outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                // Return the resulting byte array.
                return outputStream.ToArray();
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a library project this method can be removed.
    public static void Main(string[] args)
    {
        // No operation – the class is intended to be used programmatically.
    }
}

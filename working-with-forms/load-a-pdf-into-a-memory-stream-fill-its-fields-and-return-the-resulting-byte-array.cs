using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFieldFiller
{
    /// <summary>
    /// Loads a PDF from a byte array, fills the specified form fields, and returns the updated PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">The original PDF content.</param>
    /// <param name="fieldValues">Dictionary of field names and the values to set.</param>
    /// <returns>Byte array containing the filled PDF.</returns>
    public static byte[] FillFields(byte[] pdfBytes, Dictionary<string, string> fieldValues)
    {
        // Load the PDF from the input memory stream.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Document doc = new Document(inputStream))
        {
            // Iterate over the provided field values and assign them to the form fields.
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // Ensure the field exists before setting its value.
                if (doc.Form.HasField(kvp.Key))
                {
                    // The indexer returns a WidgetAnnotation in some versions; cast to Field to access the Value property.
                    if (doc.Form[kvp.Key] is Field field)
                    {
                        field.Value = kvp.Value;
                    }
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
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via PdfFieldFiller.FillFields.
    }
}

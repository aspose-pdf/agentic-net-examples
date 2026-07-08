using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFieldFiller
{
    /// <summary>
    /// Loads a PDF from a memory stream, fills the specified form fields, and returns the resulting PDF as a byte array.
    /// </summary>
    /// <param name="pdfData">Byte array containing the source PDF.</param>
    /// <param name="fieldValues">Dictionary where the key is the field name and the value is the text to set.</param>
    /// <returns>Byte array of the PDF with fields filled.</returns>
    public static byte[] FillFields(byte[] pdfData, Dictionary<string, string> fieldValues)
    {
        // Input stream containing the original PDF
        using (MemoryStream inputStream = new MemoryStream(pdfData))
        // Load the PDF document from the stream (lifecycle rule: use Document constructor with Stream)
        using (Document doc = new Document(inputStream))
        {
            // Iterate over the provided field values and set them in the form
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Aspose.Pdf.Forms.Field
                Field field = doc.Form[kvp.Key] as Field;
                if (field != null)
                {
                    // Set the field's value
                    field.Value = kvp.Value;
                }
            }

            // Prepare an output stream to hold the modified PDF
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Save the document to the output stream (lifecycle rule: Document.Save(Stream))
                doc.Save(outputStream);

                // Return the resulting byte array
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
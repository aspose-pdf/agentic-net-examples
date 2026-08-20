using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfFormHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, updates its form fields, and returns the modified PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">The original PDF content.</param>
    /// <param name="fieldValues">Dictionary where key = fully qualified field name, value = new field value.</param>
    /// <returns>Byte array containing the updated PDF.</returns>
    public static byte[] UpdateFormFields(byte[] pdfBytes, Dictionary<string, string> fieldValues)
    {
        // Validate input
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));
        if (fieldValues == null) throw new ArgumentNullException(nameof(fieldValues));

        // Load the PDF from the input byte array using a MemoryStream
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Form form = new Form(inputStream))
        {
            // Iterate over the supplied field values and fill each field
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // FillField expects the full field name and the new value as a string
                form.FillField(kvp.Key, kvp.Value);
            }

            // Save the modified PDF into an output MemoryStream
            using (MemoryStream outputStream = new MemoryStream())
            {
                form.Save(outputStream);
                // Ensure the stream position is at the beginning before reading
                outputStream.Position = 0;
                return outputStream.ToArray();
            }
        }
    }
}

// Dummy entry point so the project can be built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Example usage (optional). This block can be removed if the assembly is used as a library.
        // byte[] pdf = File.ReadAllBytes("input.pdf");
        // var values = new Dictionary<string, string> { { "Field1", "Value1" } };
        // byte[] result = PdfFormHelper.UpdateFormFields(pdf, values);
        // File.WriteAllBytes("output.pdf", result);
    }
}
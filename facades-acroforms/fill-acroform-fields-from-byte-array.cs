using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfFormHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, updates its form fields, and returns the modified PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Input PDF data.</param>
    /// <param name="fieldValues">Dictionary of fully‑qualified field names and the values to set.</param>
    /// <returns>Modified PDF data.</returns>
    public static byte[] FillFormFields(byte[] pdfBytes, Dictionary<string, string> fieldValues)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));
        if (fieldValues == null) throw new ArgumentNullException(nameof(fieldValues));

        // Input stream for the source PDF
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        // Output stream for the updated PDF
        using (MemoryStream outputStream = new MemoryStream())
        // Form facade to work with AcroForm fields
        using (Form form = new Form())
        {
            // Bind the PDF stream to the Form facade
            form.BindPdf(inputStream);

            // Update each field using the FillField(string, string) overload
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // The field name must be the full name as returned by form.FieldNames
                form.FillField(kvp.Key, kvp.Value);
            }

            // Save the modified PDF into the output stream
            form.Save(outputStream);

            // Return the resulting byte array
            return outputStream.ToArray();
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via PdfFormHelper.
    }
}
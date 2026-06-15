using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfHelper
{
    // Loads a PDF from a byte array, fills its form fields, and returns the updated PDF as a byte array.
    public static byte[] FillPdfFields(byte[] pdfBytes, Dictionary<string, string> fieldValues)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));
        if (fieldValues == null) throw new ArgumentNullException(nameof(fieldValues));

        // Load the PDF from the input memory stream.
        using (var inputStream = new MemoryStream(pdfBytes))
        {
            var doc = new Document(inputStream);

            // Populate each form field with the provided value.
            foreach (var kvp in fieldValues)
            {
                // The Form indexer returns a WidgetAnnotation; cast to Field to access the Value property.
                if (doc.Form[kvp.Key] is Field field && field != null)
                {
                    field.Value = kvp.Value;
                }
            }

            // Save the modified PDF into an output memory stream.
            using (var outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}

public class Program
{
    // Dummy entry point to satisfy the compiler for a console application.
    public static void Main(string[] args)
    {
        // Example usage (commented out – replace with real paths/values as needed).
        // byte[] pdfBytes = File.ReadAllBytes("template.pdf");
        // var values = new Dictionary<string, string> { { "FirstName", "John" }, { "LastName", "Doe" } };
        // byte[] result = PdfHelper.FillPdfFields(pdfBytes, values);
        // File.WriteAllBytes("filled.pdf", result);
    }
}
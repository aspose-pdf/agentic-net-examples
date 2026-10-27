using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFieldFiller
{
    /// <summary>
    /// Loads a PDF from a byte array, fills the specified form fields, and returns the modified PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Original PDF content.</param>
    /// <param name="fieldValues">Dictionary where key = field name, value = field value.</param>
    /// <returns>Byte array of the PDF with filled fields.</returns>
    public static byte[] FillFields(byte[] pdfBytes, Dictionary<string, string> fieldValues)
    {
        // Load the source PDF into a memory stream.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            // Ensure the stream position is at the beginning before loading.
            inputStream.Position = 0;

            // Load the PDF document from the stream.
            using (Document doc = new Document(inputStream))
            {
                // Iterate over the provided field values and set them in the PDF form.
                foreach (KeyValuePair<string, string> kvp in fieldValues)
                {
                    // The Form indexer returns a WidgetAnnotation; cast it to Field.
                    Field field = doc.Form[kvp.Key] as Field;
                    if (field == null)
                        continue; // No such field or not a form field.

                    // Strong‑typed handling for TextBoxField (stable across versions).
                    if (field is TextBoxField txtField)
                    {
                        txtField.Value = kvp.Value;
                    }
                    else
                    {
                        // For other field types use runtime type inspection to avoid version‑specific compile‑time dependencies.
                        string typeName = field.GetType().Name;

                        if (typeName == "CheckBoxField")
                        {
                            // Checkbox – treat "true" (case‑insensitive) as checked.
                            dynamic checkBox = field;
                            checkBox.Checked = kvp.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
                        }
                        else if (typeName == "RadioButtonOptionField")
                        {
                            // Radio button – set the selected option value.
                            dynamic radio = field;
                            radio.Value = kvp.Value;
                        }
                        // Additional field types (e.g., ListBox) can be added here using a similar pattern.
                    }
                }

                // Save the modified PDF into a new memory stream.
                using (MemoryStream outputStream = new MemoryStream())
                {
                    doc.Save(outputStream);
                    return outputStream.ToArray();
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project expects a Main method.
    public static void Main(string[] args)
    {
        // This method is intentionally left empty. The class is designed to be used
        // programmatically via the FillFields method.
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFieldFiller
{
    /// <summary>
    /// Loads a PDF from the provided input stream, fills the specified form fields,
    /// and returns the resulting PDF as a byte array.
    /// </summary>
    /// <param name="pdfInput">Stream containing the source PDF.</param>
    /// <param name="fieldValues">Dictionary of field names and the values to set.</param>
    /// <returns>Byte array of the filled PDF.</returns>
    public static byte[] FillFields(Stream pdfInput, Dictionary<string, string> fieldValues)
    {
        // Ensure the input stream is at the beginning
        if (pdfInput.CanSeek)
            pdfInput.Position = 0;

        // Load the PDF document from the input stream
        using (Document doc = new Document(pdfInput))
        {
            // Iterate over the supplied field values and assign them to the form fields
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
                Field? field = doc.Form[kvp.Key] as Field;
                if (field == null)
                    continue; // field not found or not a form field – skip

                // Different field types expose the value via different properties
                switch (field)
                {
                    case TextBoxField txt:
                        // TextBoxField uses the Value property (Text does not exist)
                        txt.Value = kvp.Value;
                        break;
                    case ComboBoxField combo:
                        combo.Value = kvp.Value;
                        break;
                    case ListBoxField list:
                        list.Value = kvp.Value;
                        break;
                    case CheckboxField chk:
                        // For check boxes, "On" means checked, any other value means unchecked
                        chk.Checked = string.Equals(kvp.Value, "On", StringComparison.OrdinalIgnoreCase);
                        break;
                    case RadioButtonField radio:
                        radio.Value = kvp.Value;
                        break;
                    default:
                        // Fallback – many field types inherit from the base Field which has a Value property
                        field.Value = kvp.Value;
                        break;
                }
            }

            // Save the modified document into a memory stream
            using (MemoryStream output = new MemoryStream())
            {
                doc.Save(output);
                return output.ToArray();
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // The method does nothing and can be removed if the project type is changed to a class library.
    public static void Main(string[] args)
    {
        // No operation – placeholder only.
    }
}
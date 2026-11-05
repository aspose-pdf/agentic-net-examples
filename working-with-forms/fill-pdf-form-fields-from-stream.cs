using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace AsposePdfApi
{
    public static class PdfFieldFiller
    {
        /// <summary>
        /// Loads a PDF from a byte array, fills the specified form fields, and returns the modified PDF as a byte array.
        /// </summary>
        /// <param name="pdfBytes">Original PDF content.</param>
        /// <param name="fieldValues">Dictionary where key = field name and value = value to set.</param>
        /// <returns>Byte array of the PDF with fields filled.</returns>
        public static byte[] FillFields(byte[] pdfBytes, Dictionary<string, string> fieldValues)
        {
            // Load the PDF into a memory stream
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            {
                // Open the document inside a using block for deterministic disposal
                using (Document doc = new Document(inputStream))
                {
                    // Iterate over each field to set its value
                    foreach (KeyValuePair<string, string> kvp in fieldValues)
                    {
                        // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
                        Field? field = doc.Form[kvp.Key] as Field;
                        if (field == null)
                            continue; // Skip if the field does not exist or is not a form field

                        // Strong‑typed handling for TextBoxField (stable across versions)
                        if (field is TextBoxField txtField)
                        {
                            txtField.Value = kvp.Value;
                        }
                        else
                        {
                            // Use runtime inspection / dynamic dispatch for other field types.
                            // This avoids compile‑time dependencies on types that may differ between Aspose.Pdf versions.
                            string typeName = field.GetType().Name;

                            if (typeName == "CheckBoxField")
                            {
                                // Checkbox – set the Checked property (true/false)
                                dynamic chk = field;
                                chk.Checked = kvp.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
                            }
                            else if (typeName == "RadioButtonOptionField")
                            {
                                // Radio button option – try to set a property that selects the option.
                                dynamic rad = field;
                                try
                                {
                                    // Prefer a "Checked" boolean if it exists.
                                    rad.Checked = kvp.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
                                }
                                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                                {
                                    // Fallback to a "Selected" property that may accept a string or index.
                                    try
                                    {
                                        rad.Selected = kvp.Value;
                                    }
                                    catch
                                    {
                                        // If neither property exists, ignore silently – the field will remain unchanged.
                                    }
                                }
                            }
                            else if (typeName == "ListBoxField")
                            {
                                // List box – clear previous selections and add the new one.
                                dynamic list = field;
                                try
                                {
                                    list.SelectedItems.Clear();
                                    list.SelectedItems.Add(kvp.Value);
                                }
                                catch
                                {
                                    // If the concrete type does not expose SelectedItems, ignore.
                                }
                            }
                            // Additional field types can be added here following the same pattern.
                        }
                    }

                    // Save the modified document to a new memory stream
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        doc.Save(outputStream);
                        // Return the resulting byte array
                        return outputStream.ToArray();
                    }
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // The library functionality is exposed via PdfFieldFiller.FillFields.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – this method exists solely to provide a valid entry point.
        }
    }
}

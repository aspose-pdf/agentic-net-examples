using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for XfdfReader

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "filled_output.pdf";
        const string xfdfPath = "data.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1) Manually assign values to form fields
            // -----------------------------------------------------------------
            // Iterate over all fields in the form and set a sample value.
            // Use strong‑typed handling for each concrete field type that is guaranteed
            // to exist (e.g., TextBoxField). For other field types that may vary across
            // Aspose.Pdf versions, fall back to runtime inspection and dynamic dispatch.
            foreach (Field field in doc.Form.Fields)
            {
                if (field is TextBoxField textBox)
                {
                    textBox.Value = "Sample Text";
                }
                else if (field.GetType().Name == "CheckBoxField")
                {
                    // The concrete type may not be referenced at compile time.
                    // Use dynamic to set the Checked property if it exists.
                    dynamic checkBox = field;
                    try
                    {
                        checkBox.Checked = true;
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { /* property not present – ignore */ }
                }
                else if (field.GetType().Name == "RadioButtonOptionField")
                {
                    dynamic radioOption = field;
                    try
                    {
                        radioOption.Checked = true;
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { /* property not present – ignore */ }
                }
                else
                {
                    // Fallback – try to set a generic "Value" property via reflection if it exists.
                    var valueProp = field.GetType().GetProperty("Value");
                    if (valueProp != null && valueProp.CanWrite)
                    {
                        valueProp.SetValue(field, "Default");
                    }
                }
            }

            // Example of setting a single text field by its full name.
            // Replace "CustomerName" with the actual field name in your PDF.
            if (doc.Form.HasField("CustomerName"))
            {
                if (doc.Form["CustomerName"] is TextBoxField nameField)
                {
                    nameField.Value = "John Doe";
                }
            }

            // -----------------------------------------------------------------
            // 2) Import field values from an XFDF file (optional)
            // -----------------------------------------------------------------
            // XFDF contains field names and values; this call overwrites any
            // values set above for matching fields.
            if (File.Exists(xfdfPath))
            {
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    // Import field values from XFDF into the document
                    XfdfReader.ReadFields(xfdfStream, doc);
                }
            }

            // -----------------------------------------------------------------
            // 3) Save the modified PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields processed and saved to '{outputPdfPath}'.");
    }

    // Helper that checks at runtime whether an object has a given property.
    // Kept for potential future use; not required for the current logic.
    private static bool HasProperty(object obj, string name)
    {
        return obj?.GetType().GetProperty(name) != null;
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF file (could be any source stream)
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF as a read‑only stream and create a Document from it
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        using (Document doc = new Document(pdfStream)) // lifecycle rule: use using for Document
        {
            // Check if the document contains an AcroForm
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the PDF.");
                return;
            }

            // Iterate over all form fields and output their names and values
            foreach (Field field in doc.Form)
            {
                string value = GetFieldValue(field);
                Console.WriteLine($"Field Name: {field.FullName}, Value: {value}");
            }
        }
    }

    // Helper method to extract a readable value from different field types
    static string GetFieldValue(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value ?? string.Empty;

            case CheckboxField chk:
                return chk.Checked ? "Checked" : "Unchecked";

            case RadioButtonField rad:
                return rad.Value ?? string.Empty;

            case ListBoxField list:
                // ListBoxField.SelectedItems is an int[] (indices of selected items).
                // Use the indices directly; string.Join works with any IEnumerable<T>.
                return list.SelectedItems != null ? string.Join(", ", list.SelectedItems) : string.Empty;

            case ComboBoxField combo:
                return combo.Value ?? string.Empty;

            case SignatureField sig:
                return sig.Value != null ? "Signed" : "Not signed";

            default:
                return "(unsupported field type)";
        }
    }
}

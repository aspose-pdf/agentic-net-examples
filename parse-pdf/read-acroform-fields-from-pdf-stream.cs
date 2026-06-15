using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF file as a read‑only stream.
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        {
            // Initialize a Document from the stream. No saving is performed.
            using (Document doc = new Document(pdfStream))
            {
                // Check whether the PDF contains an AcroForm.
                if (doc.Form != null && doc.Form.Count > 0)
                {
                    Console.WriteLine($"AcroForm contains {doc.Form.Count} fields:");
                    foreach (Field field in doc.Form)
                    {
                        // Retrieve the field value in a type‑safe way.
                        string value = GetFieldValue(field);
                        Console.WriteLine($"- Name: {field.FullName}, Type: {field.GetType().Name}, Value: {value}");
                    }
                }
                else
                {
                    Console.WriteLine("No AcroForm fields found.");
                }
            }
        }
    }

    // Helper method to extract a readable value from different field types.
    static string GetFieldValue(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value;
            case CheckboxField chk:
                return chk.Checked ? "Checked" : "Unchecked";
            case RadioButtonField rad:
                return rad.Value;
            case ListBoxField list:
                return string.Join(", ", list.SelectedItems);
            case ComboBoxField combo:
                return combo.Value;
            case SignatureField sig:
                return sig.Value != null ? "Signed" : "Unsigned";
            default:
                return field.Value?.ToString() ?? string.Empty;
        }
    }
}
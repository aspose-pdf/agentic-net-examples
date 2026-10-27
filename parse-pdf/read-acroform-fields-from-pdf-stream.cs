using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the source PDF (could be any stream source)
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF file as a read‑only stream and initialize a Document
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        using (Document doc = new Document(pdfStream))
        {
            // Ensure the document contains an AcroForm
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the document.");
                return;
            }

            // Iterate over all form fields and display their basic information
            foreach (Field field in doc.Form)
            {
                // Field name (full hierarchical name)
                string name = field.FullName;

                // Field type (e.g., TextBox, CheckBox, RadioButton, etc.)
                string type = field.GetType().Name;

                // Current value of the field (if applicable)
                string value = string.Empty;
                if (field is TextBoxField txt)
                    value = txt.Value;
                else if (field is CheckboxField chk)
                    value = chk.Checked ? "Checked" : "Unchecked";
                else if (field is RadioButtonField rad)
                    value = rad.Value;
                else if (field is ListBoxField lst)
                    value = string.Join(", ", lst.SelectedItems);
                else if (field is ComboBoxField cmb)
                    value = cmb.Value;
                // Add other field types as needed

                Console.WriteLine($"Field: {name}");
                Console.WriteLine($"  Type : {type}");
                Console.WriteLine($"  Value: {value}");
                Console.WriteLine();
            }
        }
    }
}
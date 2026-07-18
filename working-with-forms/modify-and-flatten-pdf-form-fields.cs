using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // PDF with form fields
        const string outputPdf = "output.pdf"; // Resulting PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document. This loads only the document structure; form fields are accessed lazily.
        Document doc = new Document(inputPdf);

        // Helper to set a field value safely.
        void SetField(string fieldName, string value)
        {
            var field = doc.Form[fieldName];
            if (field is TextBoxField txtField)
            {
                txtField.Value = value;
            }
            else if (field is CheckboxField chkField)
            {
                // For check boxes a non‑empty value checks the box.
                chkField.Checked = !string.IsNullOrEmpty(value);
            }
            else if (field != null)
            {
                // Generic fallback – many field types expose a Value property via the base Field class.
                if (field is Field genericField)
                {
                    genericField.Value = value;
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' does not support setting a value directly.");
                }
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found.");
            }
        }

        // Set values for existing form fields.
        SetField("Name", "John Doe");
        SetField("Date", DateTime.Today.ToString("yyyy-MM-dd"));

        // Delete a field that is no longer needed.
        if (doc.Form["ObsoleteField"] != null)
        {
            doc.Form.Delete("ObsoleteField");
        }
        else
        {
            Console.Error.WriteLine("Field 'ObsoleteField' not found – nothing to delete.");
        }

        // Flatten the form so that fields become part of the page content.
        doc.Form.Flatten();

        // Save the modified PDF.
        doc.Save(outputPdf);

        Console.WriteLine($"Form fields processed and saved to '{outputPdf}'.");
    }
}

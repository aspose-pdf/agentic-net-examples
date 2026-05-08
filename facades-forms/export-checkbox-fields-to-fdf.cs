using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFdf = "checkboxes.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Facade to read field information
            Form form = new Form(doc);
            // Facade to modify the form fields
            FormEditor editor = new FormEditor(doc);

            // Remove every field that is not a checkbox
            foreach (string fieldName in form.FieldNames)
            {
                // GetFieldType returns a FieldType enum, not a string
                FieldType fieldType = form.GetFieldType(fieldName);
                if (fieldType != FieldType.CheckBox)
                {
                    editor.RemoveField(fieldName);
                }
            }

            // Export the remaining fields (checkboxes only) to an FDF file
            using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
            {
                Form exportForm = new Form(doc);
                exportForm.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"Checkbox field definitions exported to '{outputFdf}'.");
    }
}

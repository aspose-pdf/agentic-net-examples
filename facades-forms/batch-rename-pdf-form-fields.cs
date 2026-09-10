using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with form fields
        const string outputPdf = "output_renamed.pdf"; // result PDF after renaming

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF as a Form to obtain the list of field names
        using (Form form = new Form(inputPdf))
        {
            // Create a FormEditor bound to the same document for editing
            using (FormEditor editor = new FormEditor(form.Document))
            {
                // Iterate over all field names and rename those that start with "Old_"
                foreach (string fieldName in form.FieldNames)
                {
                    if (fieldName.StartsWith("Old_"))
                    {
                        string newFieldName = "New_" + fieldName.Substring("Old_".Length);
                        editor.RenameField(fieldName, newFieldName);
                    }
                }

                // Save the edited PDF to the output file
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Renamed fields saved to '{outputPdf}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades; // Form class is in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF with form fields
        const string outputPdf = "output.pdf";  // PDF after renaming fields

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF form using the Form facade (implements IDisposable)
        using (Form form = new Form(inputPdf))
        {
            // Iterate over all field names in the document
            foreach (string fieldName in form.FieldNames)
            {
                // Check for the required prefix
                if (fieldName.StartsWith("Old_"))
                {
                    // Build the new field name by replacing the prefix
                    string newFieldName = "New_" + fieldName.Substring("Old_".Length);

                    // Rename the field using the facade method
                    form.RenameField(fieldName, newFieldName);
                }
            }

            // Save the modified PDF to the output path
            form.Save(outputPdf);
        }

        Console.WriteLine($"Fields renamed and saved to '{outputPdf}'.");
    }
}
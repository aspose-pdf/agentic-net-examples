using System;
using System.IO;
using Aspose.Pdf.Facades; // Form and related facades are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_filled.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF as a Form facade. The Form class implements IDisposable, so use a using block.
        using (Form form = new Form(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Enumerate all form fields and display their current values.
            // -----------------------------------------------------------------
            Console.WriteLine("Form fields and current values:");
            foreach (string fieldName in form.FieldNames)
            {
                // GetField returns the field's value as a string (empty if not set)
                string value = form.GetField(fieldName);
                Console.WriteLine($"  {fieldName} = {value}");
            }

            // -----------------------------------------------------------------
            // 2. Example: fill a specific text field if it exists.
            // -----------------------------------------------------------------
            const string targetField = "FirstName"; // adjust to a real field name in your PDF
            bool fieldExists = Array.Exists(form.FieldNames,
                                            f => f.Equals(targetField, StringComparison.OrdinalIgnoreCase));

            if (fieldExists)
            {
                // FillField returns true on success
                bool filled = form.FillField(targetField, "John Doe");
                Console.WriteLine(filled
                    ? $"Field '{targetField}' filled successfully."
                    : $"Failed to fill field '{targetField}'.");
            }
            else
            {
                Console.WriteLine($"Field '{targetField}' not found in the document.");
            }

            // -----------------------------------------------------------------
            // 3. Save the modified PDF to a new file.
            // -----------------------------------------------------------------
            form.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}
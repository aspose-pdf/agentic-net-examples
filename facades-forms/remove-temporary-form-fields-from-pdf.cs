using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing temporary fields
        const string outputPdf = "cleaned_output.pdf"; // PDF after removal

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the form to obtain the list of field names
        using (Form form = new Form(inputPdf))
        {
            // Initialize FormEditor with input and output files
            using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
            {
                // Iterate over all field names and remove those that start with "Temp_"
                foreach (string fieldName in form.FieldNames)
                {
                    if (fieldName.StartsWith("Temp_", StringComparison.Ordinal))
                    {
                        editor.RemoveField(fieldName);
                    }
                }
                // No explicit Save call is required; the output file is written when the editor is disposed.
            }
        }

        Console.WriteLine($"Temporary fields removed. Result saved to '{outputPdf}'.");
    }
}
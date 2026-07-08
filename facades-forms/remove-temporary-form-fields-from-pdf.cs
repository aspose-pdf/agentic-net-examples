using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the form to retrieve all field names
        using (Form form = new Form(inputPdf))
        {
            // Initialize FormEditor with input and output files
            using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
            {
                // Remove each field whose name starts with "Temp_"
                foreach (string fieldName in form.FieldNames)
                {
                    if (fieldName.StartsWith("Temp_", StringComparison.Ordinal))
                    {
                        editor.RemoveField(fieldName);
                    }
                }
                // FormEditor writes changes to the output file upon disposal
            }
        }

        Console.WriteLine($"Temporary fields removed. Output saved to '{outputPdf}'.");
    }
}
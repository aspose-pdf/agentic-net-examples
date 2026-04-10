using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing temporary fields
        const string outputPdf = "output.pdf";  // PDF after removal of Temp_ fields

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the form to obtain the list of all field names.
        using (Form form = new Form(inputPdf))
        {
            // Select only those fields whose names start with "Temp_".
            var tempFields = form.FieldNames
                                 .Where(name => name.StartsWith("Temp_", StringComparison.Ordinal))
                                 .ToList();

            // If there are no matching fields, simply copy the original file.
            if (tempFields.Count == 0)
            {
                File.Copy(inputPdf, outputPdf, overwrite: true);
                Console.WriteLine("No temporary fields found. File copied unchanged.");
                return;
            }

            // FormEditor works on an input file and writes to an output file.
            // The constructor that takes source and destination paths is obsolete.
            // Use the parameter‑less constructor, bind the source PDF, then call Save.
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPdf);

                // Remove each temporary field.
                foreach (string fieldName in tempFields)
                {
                    editor.RemoveField(fieldName);
                }

                // Save the modified PDF to the destination path.
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Temporary fields removed. Result saved to '{outputPdf}'.");
    }
}

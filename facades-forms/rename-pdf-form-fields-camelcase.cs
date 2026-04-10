using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with form fields
        const string outputPdf = "output_renamed.pdf"; // PDF after renaming fields

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF form for editing. The constructor binds the input file and
        // sets the target file that will be written after Save().
        using (Form form = new Form(inputPdf, outputPdf))
        {
            // Retrieve all existing field names.
            string[] fieldNames = form.FieldNames;

            foreach (string oldName in fieldNames)
            {
                // Simple camelCase conversion: lower‑case the first character.
                // More sophisticated logic can be applied if needed.
                if (string.IsNullOrEmpty(oldName))
                    continue;

                string newName = char.ToLowerInvariant(oldName[0]) + oldName.Substring(1);

                // Rename only when the name actually changes.
                if (!oldName.Equals(newName, StringComparison.Ordinal))
                {
                    form.RenameField(oldName, newName);
                    Console.WriteLine($"Renamed '{oldName}' → '{newName}'");
                }
            }

            // Persist the changes to the output file.
            form.Save();
        }

        Console.WriteLine($"Renamed PDF saved to '{outputPdf}'.");
    }
}
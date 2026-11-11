using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "renamed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // 1. Retrieve all form field names using the Form facade (FieldNames property).
            string[] fieldNames;
            using (Form form = new Form())
            {
                form.BindPdf(inputPath);
                fieldNames = form.FieldNames; // string[] of all field partial names
            }

            // 2. Use FormEditor for structural operations such as renaming fields.
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);

                foreach (string oldName in fieldNames)
                {
                    if (oldName.StartsWith("Old_"))
                    {
                        string newName = "New_" + oldName.Substring("Old_".Length);
                        editor.RenameField(oldName, newName);
                    }
                }

                // Persist the changes.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

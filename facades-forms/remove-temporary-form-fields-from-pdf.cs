using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Retrieve all form field names via the Form facade (not FormEditor)
        string[] fieldNames;
        using (var form = new Form())
        {
            form.BindPdf(inputPath);
            fieldNames = form.FieldNames; // FieldNames is a string[]
        }

        // Delete fields whose names start with "Temp_" using FormEditor
        using (var editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            foreach (var name in fieldNames.Where(n => n.StartsWith("Temp_", StringComparison.Ordinal)))
            {
                editor.RemoveField(name); // real member for deletion
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Temporary fields removed. Output saved to '{outputPath}'.");
    }
}

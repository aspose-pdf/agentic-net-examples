using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "renamed5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Read the existing field names with the Form facade (Form has a FieldNames property).
        using (var form = new Form())
        {
            form.BindPdf(inputPath);
            bool addressExists = form.FieldNames.Contains("Address");

            // FormEditor is used for structural changes such as renaming a field.
            using (var editor = new FormEditor())
            {
                editor.BindPdf(inputPath);

                if (addressExists)
                {
                    editor.RenameField("Address", "StreetAddress");
                }
                else
                {
                    Console.WriteLine("Field 'Address' not found; no rename performed.");
                }

                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Renamed PDF saved to '{outputPath}'.");
    }
}

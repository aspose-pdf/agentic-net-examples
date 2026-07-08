using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF form and specify the output file
        using (Form form = new Form(inputPath, outputPath))
        {
            // Iterate over all field names
            foreach (string fieldName in form.FieldNames)
            {
                // Rename fields that start with "Old_"
                if (fieldName.StartsWith("Old_"))
                {
                    string newName = "New_" + fieldName.Substring("Old_".Length);
                    form.RenameField(fieldName, newName);
                }
            }

            // Save the updated PDF
            form.Save();
        }

        Console.WriteLine($"Renamed fields saved to '{outputPath}'.");
    }
}
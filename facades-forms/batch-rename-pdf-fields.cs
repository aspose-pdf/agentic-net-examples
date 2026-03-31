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

        Form form = new Form(inputPath, outputPath);

        foreach (string fieldName in form.FieldNames)
        {
            if (string.IsNullOrEmpty(fieldName))
                continue;

            // Convert to camelCase: lower case first character, keep the rest unchanged
            string newFieldName = char.ToLowerInvariant(fieldName[0]) + fieldName.Substring(1);
            if (newFieldName != fieldName)
            {
                form.RenameField(fieldName, newFieldName);
                Console.WriteLine($"Renamed '{fieldName}' to '{newFieldName}'");
            }
        }

        form.Save();
        Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
    }
}
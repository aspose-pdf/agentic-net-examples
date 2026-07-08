using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF form and specify the output file.
        using (Form form = new Form(inputPath, outputPath))
        {
            // Iterate over all existing field names.
            foreach (string oldName in form.FieldNames)
            {
                string newName = ToCamelCase(oldName);

                // Rename only if the new name differs from the original.
                if (!string.Equals(oldName, newName, StringComparison.Ordinal))
                {
                    form.RenameField(oldName, newName);
                }
            }

            // Persist the changes.
            form.Save();
        }

        Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
    }

    // Converts a string to camelCase (e.g., "First_Name" -> "firstName").
    private static string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Split on common delimiters: underscore, space, hyphen.
        string[] parts = Regex.Split(input, @"[_\s-]+");

        // Capitalize each part.
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].Length == 0) continue;
            parts[i] = char.ToUpper(parts[i][0]) + (parts[i].Length > 1 ? parts[i].Substring(1) : string.Empty);
        }

        // Concatenate parts.
        string combined = string.Concat(parts);

        // Lowercase the first character to achieve camelCase.
        return char.ToLower(combined[0]) + combined.Substring(1);
    }
}
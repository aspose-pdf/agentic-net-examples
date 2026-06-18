using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Converts a field name to camelCase (e.g., "First_Name" -> "firstName")
    static string ToCamelCase(string input)
    {
        // Split on non‑letter/digit characters
        string[] parts = Regex.Split(input, "[^A-Za-z0-9]+");
        if (parts.Length == 0) return input;

        var sb = new StringBuilder();

        // First part: all lower case
        sb.Append(parts[0].ToLowerInvariant());

        // Subsequent parts: first letter upper case, rest lower case
        for (int i = 1; i < parts.Length; i++)
        {
            if (string.IsNullOrEmpty(parts[i])) continue;
            sb.Append(char.ToUpperInvariant(parts[i][0]));
            if (parts[i].Length > 1)
                sb.Append(parts[i].Substring(1).ToLowerInvariant());
        }

        return sb.ToString();
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF with form fields
        const string outputPdf = "output_renamed.pdf"; // destination PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document to obtain the list of field names.
        Document pdfDoc = new Document(inputPdf);

        // Create a FormEditor and bind the same PDF (the editor works on the file path).
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);

            // Retrieve all existing field names via the Document object.
            string[] fieldNames = pdfDoc.Form.Fields
                                          .Select(f => f.PartialName)
                                          .ToArray();

            foreach (string oldName in fieldNames)
            {
                string newName = ToCamelCase(oldName);

                // Only rename if the new name differs from the old one
                if (!string.Equals(oldName, newName, StringComparison.Ordinal))
                {
                    editor.RenameField(oldName, newName);
                }
            }

            // Persist changes to the output file using the Save overload that accepts a file path.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Fields renamed and saved to '{outputPdf}'.");
    }
}

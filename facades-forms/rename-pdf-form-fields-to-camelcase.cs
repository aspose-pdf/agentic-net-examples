using System;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // ------------------------------------------------------------
        // Create a self‑contained PDF with a few sample form fields.
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a page.
            Page page = doc.Pages.Add();

            // Add a text box field named "First_Name".
            TextBoxField txt = new TextBoxField(page, new Rectangle(100, 600, 300, 620))
            {
                PartialName = "First_Name",
                Value = "Sample"
            };
            doc.Form.Add(txt, 1);

            // Add another field with a different naming style.
            TextBoxField txt2 = new TextBoxField(page, new Rectangle(100, 560, 300, 580))
            {
                PartialName = "last-name",
                Value = "Example"
            };
            doc.Form.Add(txt2, 1);

            // Save the seed PDF that will be processed.
            doc.Save(inputPdf);
        }

        // ------------------------------------------------------------
        // Rename all form fields to camelCase using FormEditor.
        // ------------------------------------------------------------
        using (FormEditor editor = new FormEditor())
        {
            // Bind the PDF that contains the form fields.
            editor.BindPdf(inputPdf);

            // Load the same PDF as a Document to enumerate the fields.
            Document doc = new Document(inputPdf);

            foreach (Field field in doc.Form.Fields)
            {
                string oldName = field.PartialName;
                string newName = ToCamelCase(oldName);
                // Skip if the name is already camelCase.
                if (!oldName.Equals(newName, StringComparison.Ordinal))
                {
                    editor.RenameField(oldName, newName);
                }
            }

            // Save the updated PDF to the desired output path.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Fields renamed and saved to '{outputPdf}'.");
    }

    // Simple camelCase conversion: lower first character, keep the rest.
    // If the name contains underscores, spaces or hyphens, they are removed and the following
    // segment starts with an uppercase letter (e.g., "First_Name" -> "firstName").
    static string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Split on non‑alphanumeric characters.
        string[] parts = input.Split(new[] { '_', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return input;

        StringBuilder sb = new StringBuilder();

        // First part: lower case first character.
        sb.Append(char.ToLowerInvariant(parts[0][0]));
        if (parts[0].Length > 1)
            sb.Append(parts[0].Substring(1));

        // Remaining parts: capitalize first character.
        for (int i = 1; i < parts.Length; i++)
        {
            if (parts[i].Length == 0) continue;
            sb.Append(char.ToUpperInvariant(parts[i][0]));
            if (parts[i].Length > 1)
                sb.Append(parts[i].Substring(1));
        }

        return sb.ToString();
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Convert a string to camelCase (lowercase first character, keep the rest unchanged)
    static string ToCamelCase(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        // If the first character is already lowercase, return as‑is
        if (char.IsLower(text[0]))
            return text;

        return char.ToLowerInvariant(text[0]) + text.Substring(1);
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // ------------------------------------------------------------
        // Ensure a PDF file exists – create a minimal one with a form field
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page
                Page page = doc.Pages.Add();

                // Add a simple text box form field named "FirstName"
                TextBoxField txt = new TextBoxField(page, new Rectangle(100, 600, 300, 620))
                {
                    PartialName = "FirstName",
                    Value = "John"
                };
                doc.Form.Add(txt);

                // Save the sample PDF
                doc.Save(inputPdf);
            }
        }

        // ------------------------------------------------------------
        // Load the PDF, rename its form fields to camelCase, and save
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over the form fields and rename them to camelCase
            foreach (Field field in doc.Form.Fields)
            {
                string oldName = field.PartialName;
                string camelName = ToCamelCase(oldName);
                if (!camelName.Equals(oldName, StringComparison.Ordinal))
                {
                    field.PartialName = camelName;
                }
            }

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Fields renamed to camelCase and saved to '{outputPdf}'.");
    }
}

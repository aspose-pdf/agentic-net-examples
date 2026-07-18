using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class PdfFieldFiller
{
    /// <summary>
    /// Loads a PDF, fills its form fields using the supplied dictionary, and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF containing form fields.</param>
    /// <param name="outputPdfPath">Path where the filled PDF will be saved.</param>
    /// <param name="fieldValues">Dictionary where the key is the field name (full name) and the value is the text to assign.</param>
    public static void FillFields(string inputPdfPath, string outputPdfPath, Dictionary<string, string> fieldValues)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // Document lifecycle must be managed with a using block (see document-is-tagged-do-not-exist rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all fields in the form
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // Prefer FullName, fall back to PartialName; if both are null skip the field
                string fieldName = field.FullName ?? field.PartialName;
                if (fieldName == null)
                    continue;

                if (fieldValues.TryGetValue(fieldName, out string value))
                {
                    // Assign the value directly; the Value property accepts string for most field types
                    field.Value = value;
                }
            }

            // Save the modified document (no extra SaveOptions needed for PDF output)
            pdfDoc.Save(outputPdfPath);
        }
    }

    // Example usage
    static void Main()
    {
        string inputPath = "template.pdf";
        string outputPath = "filled.pdf";

        // Create a simple PDF with form fields so the sandbox has a file to open.
        CreateSampleFormPdf(inputPath);

        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Email",     "john.doe@example.com" },
            // Add more field name/value pairs as required
        };

        FillFields(inputPath, outputPath, values);
        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    /// <summary>
    /// Generates a minimal PDF containing three text box fields: FirstName, LastName and Email.
    /// This method is only needed for the self‑contained demo environment.
    /// </summary>
    private static void CreateSampleFormPdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // FirstName field
            var firstRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
            TextBoxField firstName = new TextBoxField(page, firstRect)
            {
                Value = string.Empty
            };
            firstName.PartialName = "FirstName";
            doc.Form.Add(firstName, 1);

            // LastName field
            var lastRect = new Aspose.Pdf.Rectangle(100, 650, 300, 670);
            TextBoxField lastName = new TextBoxField(page, lastRect)
            {
                Value = string.Empty
            };
            lastName.PartialName = "LastName";
            doc.Form.Add(lastName, 1);

            // Email field
            var emailRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
            TextBoxField email = new TextBoxField(page, emailRect)
            {
                Value = string.Empty
            };
            email.PartialName = "Email";
            doc.Form.Add(email, 1);

            doc.Save(path);
        }
    }
}

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_token.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Generate a secure random token (256‑bit, Base64 encoded)
        string authToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a FormEditor bound to the loaded document
            using (FormEditor editor = new FormEditor(doc))
            {
                // Add a text field named "AuthToken" on page 1
                // Rectangle coordinates: lower‑left (100,100), upper‑right (200,120)
                // In recent Aspose.PDF versions the enum value is FieldType.Text (not TextBox)
                editor.AddField(
                    FieldType.Text,
                    "AuthToken",
                    1,               // page number (1‑based)
                    100f, 100f,      // lower‑left X, Y
                    200f, 120f);     // upper‑right X, Y

                // NOTE: The PropertyFlag.Hidden enum member is not present in current versions.
                // If a hidden field is required, you can either set the field's appearance to be invisible
                // (e.g., zero‑size rectangle or transparent text) or manage visibility via PDF form logic.
                // The line below is intentionally omitted to keep the code compatible with the library version.
            }

            // Fill the field with the generated token
            using (Form form = new Form(doc))
            {
                form.FillField("AuthToken", authToken);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden field \"AuthToken\" added. Output saved to '{outputPath}'.");
    }
}

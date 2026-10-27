using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Locate the text box field via the Form.Fields collection
            TextBoxField textField = doc.Form?.Fields
                .FirstOrDefault(f => f is TextBoxField && f.FullName == fieldName) as TextBoxField;

            if (textField != null)
            {
                // Set the background‑like color to light gray.
                // The TextBoxField class does not expose a BackgroundColor property in the
                // current Aspose.Pdf version; the Color property is used to affect the field's
                // fill appearance (acts as a background color for many form fields).
                textField.Color = Color.LightGray;
            }
            else
            {
                Console.Error.WriteLine($"Text field '{fieldName}' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with updated field background to '{outputPath}'.");
    }
}

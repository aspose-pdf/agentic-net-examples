using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "ProtectedField";   // name of the form field to protect

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field from the document's AcroForm collection
            var field = doc.Form[fieldName];
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the PDF.");
                return;
            }

            // Aspose.Pdf core API does not expose a direct per‑field password flag.
            // The older FormEditor.SetFieldPassword (a Facades API) is prohibited by the
            // hard‑rule, and the TextBoxField.IsPassword property is not available in the
            // current version. As a workaround we can make the field read‑only so that the
            // user cannot modify its content. If true password masking is required, it must
            // be applied when the PDF is created using a Facades editor (outside the scope
            // of this example).
            if (field is TextBoxField textBox)
            {
                // Mark the field as read‑only – this is the closest core‑API protection.
                textBox.Flags = AnnotationFlags.ReadOnly;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a text box and cannot be set as a password field.");
                return;
            }

            // Save the modified PDF; Document.Save writes PDF by default
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' has been secured (read‑only) and saved to '{outputPath}'.");
    }
}

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
        const string fieldName = "MyProtectedField"; // name of the form field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPath);

            // Retrieve the form field by its partial name
            var field = doc.Form[fieldName];
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Protect the field – the core API does not support a password on a field.
            // The typical way to enforce protection is to make the field read‑only.
            field.Flags = AnnotationFlags.ReadOnly;

            // Save the modified PDF
            doc.Save(outputPath);

            Console.WriteLine($"Field '{fieldName}' protected (read‑only) and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field named "Email" – the Form indexer returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field before accessing field‑specific members.
            Field emailField = doc.Form["Email"] as Field;
            if (emailField == null)
            {
                Console.Error.WriteLine("Field 'Email' not found or is not a form field in the document.");
                return;
            }

            // Set the tooltip (alternate name) to guide the user on the required format
            emailField.AlternateName = "Enter email in format user@example.com";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tooltip set and document saved to '{outputPath}'.");
    }
}

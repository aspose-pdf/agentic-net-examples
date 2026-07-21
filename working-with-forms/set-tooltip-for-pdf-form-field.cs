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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field named "Email" – the Form indexer returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field before accessing field‑specific members.
            Field emailField = doc.Form["Email"] as Field;
            if (emailField != null)
            {
                // Set the tooltip (displayed as alternate name in Acrobat)
                emailField.AlternateName = "Enter email in format user@example.com";
            }
            else
            {
                Console.WriteLine("Email field not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip at '{outputPath}'.");
    }
}

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
            // Retrieve the form field named "Email"
            // The Form indexer returns a WidgetAnnotation, so cast it to Field.
            Field field = doc.Form["Email"] as Field;
            if (field == null)
            {
                Console.Error.WriteLine("Field 'Email' not found or is not a form field.");
            }
            else
            {
                // Set the tooltip via the AlternateName property.
                if (field is TextBoxField txtField)
                {
                    txtField.AlternateName = "Enter email in format user@example.com";
                }
                else if (field is PasswordBoxField pwdField)
                {
                    pwdField.AlternateName = "Enter email in format user@example.com";
                }
                else
                {
                    // Fallback for other field types that expose AlternateName directly.
                    field.AlternateName = "Enter email in format user@example.com";
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tooltip set and saved to '{outputPath}'.");
    }
}
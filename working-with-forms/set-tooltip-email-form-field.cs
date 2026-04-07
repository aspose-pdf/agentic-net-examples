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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field named "Email"
            Field emailField = doc.Form["Email"] as Field; // cast WidgetAnnotation to Field
            if (emailField != null)
            {
                // Set the tooltip (alternate name) to guide the user
                emailField.AlternateName = "Enter email in format user@example.com";
            }
            else
            {
                Console.WriteLine("Email field not found or not a form field.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip at '{outputPath}'.");
    }
}

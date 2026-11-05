using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "Email"
            if (doc.Form["Email"] is TextBoxField emailField)
            {
                // Set the tooltip (alternate name) that appears when the user hovers over the field
                emailField.AlternateName = "Enter email in format user@example.com";
            }
            else
            {
                Console.Error.WriteLine("The 'Email' field was not found or is not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tooltip set and saved to '{outputPath}'.");
    }
}

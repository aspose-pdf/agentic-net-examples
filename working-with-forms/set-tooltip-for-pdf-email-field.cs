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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the existing field named "Email"
            // The Form indexer returns a generic Field; cast to TextBoxField to access tooltip property
            if (doc.Form["Email"] is TextBoxField emailField)
            {
                // Set the tooltip (alternate name) that appears in Adobe Acrobat
                emailField.AlternateName = "Enter email in format user@example.com";
            }
            else
            {
                Console.Error.WriteLine("Email field not found or is not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip set: {outputPath}");
    }
}
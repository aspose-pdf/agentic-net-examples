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
            // Retrieve the form field named "Email"
            // The Email field is typically a TextBoxField. Use the Contents property to set the tooltip.
            if (doc.Form["Email"] is TextBoxField emailField)
            {
                // Set the tooltip text to guide the user on the required format
                emailField.Contents = "Enter email in format: user@example.com";
            }
            else
            {
                Console.WriteLine("Email field not found or is not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip at '{outputPath}'.");
    }
}

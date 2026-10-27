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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "Email"
            TextBoxField emailField = doc.Form["Email"] as TextBoxField;
            if (emailField != null)
            {
                // Set tooltip (popup text) to guide users on the required input format
                // The Tooltip property is not available in this version; use the Contents property instead.
                emailField.Contents = "Enter a valid email address (e.g., user@example.com)";
            }
            else
            {
                Console.Error.WriteLine("Email field not found or is not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip to '{outputPath}'.");
    }
}

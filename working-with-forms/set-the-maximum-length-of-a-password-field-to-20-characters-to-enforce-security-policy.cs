using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF containing the password field
        const string outputPath = "output.pdf";  // destination PDF
        const string fieldName  = "PasswordField"; // full name of the password field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a FormEditor facade bound to the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Set the maximum character length of the password field to 20
            // PasswordBoxField inherits from TextBoxField, so SetFieldLimit works.
            bool result = formEditor.SetFieldLimit(fieldName, 20);
            if (!result)
            {
                Console.Error.WriteLine($"Failed to set field limit for '{fieldName}'.");
                return;
            }

            // Persist the changes
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Password field '{fieldName}' max length set to 20 characters. Saved to '{outputPath}'.");
    }
}
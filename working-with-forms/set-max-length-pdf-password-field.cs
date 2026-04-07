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
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the password field will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a text box field to act as a password field
            TextBoxField pwdField = new TextBoxField(page, rect)
            {
                PartialName = "Password", // field name
                MaxLen = 20               // enforce maximum length of 20 characters
            };

            // Add the field to the document's form collection
            doc.Form.Add(pwdField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with password field max length set to 20 characters: {outputPath}");
    }
}
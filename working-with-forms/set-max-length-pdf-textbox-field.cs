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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the password field will appear
            // (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a text box field on the page and enforce a maximum length of 20 characters
            TextBoxField pwdField = new TextBoxField(page, rect)
            {
                PartialName = "Password", // field name
                MaxLen = 20                // enforce maximum length of 20 characters
                // Note: The IsPassword property is not available in the current Aspose.PDF version.
            };

            // Add the field to the document's form collection
            doc.Form.Add(pwdField);

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with password field max length set to 20 characters: {outputPath}");
    }
}

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

        // Load the existing PDF document (using the required load pattern)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the new form field will appear.
            // Constructor parameters: left, bottom, width, height.
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 200, 530);

            // Create a text box form field on page 1.
            TextBoxField textField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                PartialName = "SampleTextBox", // field name
                Value       = "Enter text here"
            };

            // Add the field to the form collection on page 1.
            // This places the field in the document and registers it in the Form collection.
            doc.Form.Add(textField, 1);

            // If a specific index within the Form collection is required,
            // the Form class does not expose an Insert method.
            // The typical approach is to add the field (as above) and then
            // reorder the collection manually if needed. For most scenarios,
            // adding the field directly is sufficient for ordered layout.

            // Save the modified PDF (using the required save pattern)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new form field at '{outputPath}'.");
    }
}
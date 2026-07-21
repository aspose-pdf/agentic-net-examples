using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Create a new text box form field on page 1
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 250, 550);
            TextBoxField textField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "MyTextBox",
                Value = "Enter text here"
            };

            // Insert the new field at a specific index (e.g., index 0) in the Form collection.
            // The Form class provides an Add overload that accepts an index.
            doc.Form.Add(textField, 0);

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new form field at '{outputPath}'.");
    }
}

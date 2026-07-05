using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a new text box form field on page 1
            // Rectangle constructor: (llx, lly, urx, ury)
            TextBoxField textBox = new TextBoxField(doc.Pages[1],
                new Rectangle(100, 500, 300, 530))
            {
                PartialName = "CustomerName",   // field name
                Value       = "Enter name here"
            };

            // Add the field to the form collection.
            // The Add(Field, string, int) overload returns the added field.
            // The order of fields in the collection follows the order of insertion,
            // so adding the field at this point places it at the desired index.
            doc.Form.Add(textBox, textBox.PartialName, 1);

            // Optionally, add an additional appearance (e.g., on another page)
            // doc.Form.AddFieldAppearance(textBox, 2, new Rectangle(100, 400, 300, 430));

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with new form field saved to '{outputPath}'.");
    }
}
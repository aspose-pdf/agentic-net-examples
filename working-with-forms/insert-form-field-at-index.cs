using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the new field will appear (left, bottom, right, top)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a new text box field on page 1
            TextBoxField textBox = new TextBoxField(doc.Pages[1], fieldRect)
            {
                PartialName = "MyTextBox",
                Value = "Default text"
            };

            // Add the field to the form on page 1.
            // Fields are stored in the order they are added, so this insertion
            // determines its position in the Document.Form collection.
            doc.Form.Add(textBox, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field added and saved to '{outputPath}'.");
    }
}
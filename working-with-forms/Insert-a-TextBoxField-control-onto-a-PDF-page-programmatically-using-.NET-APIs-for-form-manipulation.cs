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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object (creates one if it doesn't exist)
            Form form = doc.Form;

            // Define the rectangle where the text box will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a TextBoxField on the first page using the (Page, Rectangle) constructor
            TextBoxField textBox = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName   = "SampleTextBox",          // field name
                AlternateName = "Enter text here",        // tooltip
                Multiline     = true,                    // allow multiple lines
                MaxLen        = 200,                     // maximum characters
                Value         = "Default text"           // initial content
            };

            // Add the field to the form (the field is already positioned on the page)
            form.Add(textBox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"TextBoxField added and saved to '{outputPath}'.");
    }
}
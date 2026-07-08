using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the text box field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field with placeholder text
            TextBoxField textBox = new TextBoxField(doc, fieldRect)
            {
                // Placeholder text – set as the initial value of the field
                Value = "Enter your name here"
            };

            // Add the field to the document's form
            doc.Form.Add(textBox);

            // --- Tagging the field as a /Form element for accessibility ---

            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root structure element (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Create a Form structure element
            FormElement formElement = taggedContent.CreateFormElement();

            // Associate the form element with the field annotation
            formElement.Tag(textBox);

            // Append the Form element to the structure tree
            root.AppendChild(formElement);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}
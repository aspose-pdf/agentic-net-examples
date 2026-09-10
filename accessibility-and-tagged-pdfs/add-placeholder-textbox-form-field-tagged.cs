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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Create a TextBoxField (form field) on the first page
            // -----------------------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing
            // Define the rectangle where the field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create the field; use the constructor that takes Document + Rectangle
            TextBoxField textBox = new TextBoxField(doc, fieldRect)
            {
                // Set a placeholder value that will be shown when the field is empty
                Value = "Enter your text here..."
            };

            // Add the field to the document's form collection
            doc.Form.Add(textBox);

            // -----------------------------------------------------------------
            // 2. Create a /Form structure element and associate it with the field
            // -----------------------------------------------------------------
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document has a tagged structure root
            StructureElement root = taggedContent.RootElement;

            // Create a FormElement (represents a widget annotation in the logical structure)
            FormElement formElement = taggedContent.CreateFormElement();

            // Append the FormElement to the root of the structure tree
            root.AppendChild(formElement);

            // Bind the form field annotation to the FormElement
            formElement.Tag(textBox);

            // -----------------------------------------------------------------
            // 3. Save the modified PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}
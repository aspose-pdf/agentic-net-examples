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

        // Load the existing PDF (lifecycle rule: use Document constructor inside a using block)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the text box will appear (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextBoxField on the document with the specified rectangle
            TextBoxField textBox = new TextBoxField(doc, fieldRect);

            // Set placeholder text (the initial value shown in the field)
            textBox.Value = "Enter your name here";

            // Add the field to the document's form (Form.Add method)
            doc.Form.Add(textBox);

            // Access the tagged content API (ITaggedContent) to create a /Form structure element
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root element of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a FormElement (represents a /Form element in the structure tree)
            FormElement formElement = taggedContent.CreateFormElement();

            // Append the FormElement to the root of the structure tree
            root.AppendChild(formElement); // AppendChild with one argument (default bool)

            // Associate the form field with the /Form element (Tag binds the annotation to the structure element)
            formElement.Tag(textBox);

            // Save the modified PDF (lifecycle rule: save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}
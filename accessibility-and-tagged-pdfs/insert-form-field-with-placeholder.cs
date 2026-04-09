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
            // Create a text box form field with a placeholder value
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                // Placeholder text (initial value shown in the field)
                Value = "Enter your name here"
            };

            // Add the field to the document's form
            doc.Form.Add(txtField);

            // Ensure the document is tagged (if not already)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Create a Form structure element and associate it with the field annotation
            FormElement formElem = tagged.CreateFormElement();
            formElem.AlternativeText = "User name input field";

            // Tag the field annotation with the Form element
            formElem.Tag(txtField);

            // Append the Form element to the root of the structure tree
            StructureElement root = tagged.RootElement;
            root.AppendChild(formElem);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}
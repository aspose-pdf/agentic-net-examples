using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Annotations; // needed for Border

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // existing PDF (can be empty)
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Create a form field (TextBox) and add it to the AcroForm.
            // -----------------------------------------------------------------
            // Define the field rectangle (coordinates are in points, origin bottom‑left)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the field. The constructor (Document, Rectangle) registers the field
            // with the document but does NOT add it to the Form collection yet.
            TextBoxField textField = new TextBoxField(doc, fieldRect)
            {
                // Set a name that will be used in the form hierarchy
                PartialName = "MyTextField",
                // Optional visual properties
                Color = Aspose.Pdf.Color.LightGray
            };

            // Border must be created after the field instance exists because it requires the parent annotation.
            textField.Border = new Border(textField) { Width = 1 };

            // Add the field to the document's AcroForm collection.
            // This registers the field in the PDF's /AcroForm dictionary.
            doc.Form.Add(textField);

            // -----------------------------------------------------------------
            // 2. Create a /Form structure element and associate it with the field.
            // -----------------------------------------------------------------
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a FormElement (represents a widget annotation in the structure)
            FormElement formStruct = taggedContent.CreateFormElement();

            // Provide alternative text for accessibility
            formStruct.AlternativeText = "Text box for user input";

            // Append the FormElement to the root (or any other appropriate parent)
            root.AppendChild(formStruct);

            // Link the widget annotation (the field) with the structure element.
            // Use the Tag method to associate the field with the /Form element.
            formStruct.Tag(textField);

            // -----------------------------------------------------------------
            // 3. Save the modified PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field and /Form structure element saved to '{outputPath}'.");
    }
}

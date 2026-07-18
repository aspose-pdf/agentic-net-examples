using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

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

        // Load the existing PDF (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Create a text box form field and add it to the AcroForm
            // -------------------------------------------------
            // Define the field rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the field (constructor that accepts Document and Rectangle)
            TextBoxField textField = new TextBoxField(doc, fieldRect)
            {
                // Set a partial name (field identifier)
                PartialName = "UserName",
                // Optional: set default appearance (font, size, color)
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Register the field in the document's AcroForm
            doc.Form.Add(textField);

            // -------------------------------------------------
            // 2. Create a /Form structure element and associate it with the field
            // -------------------------------------------------
            ITaggedContent taggedContent = doc.TaggedContent;

            // Create a FormElement (represents a widget annotation in the structure tree)
            FormElement formStruct = taggedContent.CreateFormElement();

            // Provide alternative text for accessibility
            formStruct.AlternativeText = "User name input field";

            // Append the FormElement to the root of the structure tree
            StructureElement root = taggedContent.RootElement;
            root.AppendChild(formStruct);

            // Associate the widget annotation (the field) with the structure element
            // Use FormElement.Tag to link the field to the structure element
            formStruct.Tag(textField);

            // -------------------------------------------------
            // 3. Save the modified PDF (lifecycle rule: save inside using)
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}

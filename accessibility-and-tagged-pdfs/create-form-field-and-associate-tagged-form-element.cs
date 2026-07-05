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
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Create a text box form field and add it to the AcroForm
            // -------------------------------------------------
            // Define the field rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the field (constructor that accepts Document and rectangle)
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                Name = "UserName",          // field name
                Value = "Enter name here"    // default value
            };

            // Register the field in the document's AcroForm
            doc.Form.Add(txtField);

            // -------------------------------------------------
            // 2. Create a /Form structure element and attach it to the field
            // -------------------------------------------------
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document has a root structure element
            StructureElement root = taggedContent.RootElement;

            // Create a Form structure element (widget annotation representing a form field)
            FormElement formStruct = taggedContent.CreateFormElement();
            formStruct.AlternativeText = "User name input field";

            // Append the Form element to the root of the structure tree
            root.AppendChild(formStruct);

            // Associate the widget annotation (field) with the structure element.
            // Use the Tag method to link the field to the /Form element.
            formStruct.Tag(txtField);

            // -------------------------------------------------
            // 3. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}

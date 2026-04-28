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
        const string outputPath = "form_tagged.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the form field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field on the page
            TextBoxField txtField = new TextBoxField(page, fieldRect)
            {
                PartialName = "MyTextField",   // field name
                Value       = "Enter text here"
            };

            // Register the field in the AcroForm (document's form collection)
            doc.Form.Add(txtField);

            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with a tagged form field");

            // Create a /Form structure element
            FormElement formStruct = tagged.CreateFormElement();
            formStruct.AlternativeText = "User input text field";

            // Associate the widget annotation (the form field) with the structure element
            // Use the Tag method – FormElement does not expose an Annotation property.
            formStruct.Tag(txtField);

            // Append the structure element to the root of the structure tree
            StructureElement root = tagged.RootElement;
            root.AppendChild(formStruct);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // <-- added
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "form_placeholder.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define the rectangle for the textbox field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a textbox field with placeholder text
            // NOTE: TextBoxField must be instantiated with a Page object and a Rectangle
            TextBoxField txtField = new TextBoxField(page, rect);
            txtField.PartialName = "NameField";
            txtField.Value = "Enter your name"; // placeholder/default value

            // Add the field to the document's form
            doc.Form.Add(txtField);

            // ----- Tagged PDF part: associate the field with a /Form structure element -----
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Form with placeholder");

            // Create a Form element in the logical structure tree
            FormElement formElem = tagged.CreateFormElement();
            // Tag the field's widget annotation with the Form element (field itself is a widget annotation)
            formElem.Tag(txtField);

            // Append the Form element to the root of the structure tree
            StructureElement root = tagged.RootElement;
            root.AppendChild(formElem);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

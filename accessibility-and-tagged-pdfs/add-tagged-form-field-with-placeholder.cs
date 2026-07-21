using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Tagged;              // ITaggedContent interface
using Aspose.Pdf.LogicalStructure;    // StructureElement, FormElement
using Aspose.Pdf.Forms;               // TextBoxField

class Program
{
    static void Main()
    {
        // Input PDF (must exist) and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the form field will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle for the text box field (left, bottom, right, top)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a TextBoxField widget annotation on the selected page
            TextBoxField textField = new TextBoxField(page, fieldRect);

            // Set placeholder text (initial value shown in the field)
            textField.Value = "Enter your name";

            // Add the field to the document's form (lifecycle rule: add)
            doc.Form.Add(textField);

            // Access the tagged content API to create a /Form structure element
            ITaggedContent taggedContent = doc.TaggedContent;

            // Create a FormElement (represents a widget annotation in the logical structure)
            FormElement formElement = taggedContent.CreateFormElement();

            // Provide an alternative description for accessibility tools
            formElement.AlternativeText = "Name input field";

            // Bind the FormElement to the actual widget annotation (field)
            formElement.Tag(textField);

            // Append the FormElement to the root of the structure tree
            StructureElement root = taggedContent.RootElement;
            root.AppendChild(formElement);   // AppendChild with one argument (default bool)

            // Save the modified PDF (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}
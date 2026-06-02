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

        // Open the existing PDF (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the text box will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box form field on the page
            TextBoxField textField = new TextBoxField(page, rect)
            {
                // Placeholder text (initial value shown in the field)
                Value = "Enter your name here"
            };

            // Add the field to the document's form
            doc.Form.Add(textField);

            // ----- Tagged PDF part -----
            // Access tagged content (no extra using for a non‑existent namespace)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Create a Form structure element (represents a widget annotation)
            FormElement formElement = taggedContent.CreateFormElement();

            // Associate the form field annotation with the Form element
            formElement.Tag(textField);

            // Append the Form element to the root of the structure tree
            StructureElement root = taggedContent.RootElement;
            root.AppendChild(formElement); // AppendChild with one argument (default bool)

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}
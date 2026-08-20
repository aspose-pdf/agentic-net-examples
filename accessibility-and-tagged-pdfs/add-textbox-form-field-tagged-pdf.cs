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
        const string outputPath = "output_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has an AcroForm object
            Form acroForm = doc.Form;

            // Define the rectangle where the field will be placed
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field on the document
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                Name = "SampleTextBox",
                Value = "Enter text here"
            };

            // Register the field in the AcroForm
            acroForm.Add(txtField);

            // ----- Tagged PDF part -----
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Form with TextBox");

            // Get the root structure element
            StructureElement root = tagged.RootElement;

            // Create a Form structure element (represents a widget annotation)
            FormElement formStruct = tagged.CreateFormElement();

            // Associate the widget annotation (the text box field) with the structure element
            // NOTE: Form fields are themselves annotations, so we pass the field directly.
            formStruct.Tag(txtField);

            // Provide alternative text for accessibility
            formStruct.AlternativeText = "Text box for user input";

            // Append the Form element to the structure tree
            root.AppendChild(formStruct);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with form field to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Prepare tagged content (language and title)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(System.IO.Path.GetFileNameWithoutExtension(inputPath));

            // Create a text box form field on page 1
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                Name = "SampleText",
                Value = "Enter text here",
                AlternateName = "Sample Text Field"
            };
            // Register the field in the AcroForm (page numbers are 1‑based)
            doc.Form.Add(txtField, 1);

            // Create a /Form structure element and attach it to the root
            StructureElement root = tagged.RootElement;
            FormElement formStruct = tagged.CreateFormElement();
            formStruct.AlternativeText = "Form field structure element";
            root.AppendChild(formStruct);

            // Associate the widget annotation with the structure element
            formStruct.Tag(txtField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with form saved to '{outputPath}'.");
    }
}

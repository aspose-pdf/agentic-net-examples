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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // ----- Create a text box form field -----
            var fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            var txtField = new TextBoxField(doc, fieldRect)
            {
                PartialName = "UserName",
                Value = "Enter name",
                Color = Aspose.Pdf.Color.LightGray
            };
            // Register the field in the AcroForm
            doc.Form.Add(txtField);

            // ----- Create a /Form structure element for accessibility -----
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;
            FormElement formStruct = tagged.CreateFormElement();
            formStruct.AlternativeText = "User name text field";
            root.AppendChild(formStruct);

            // ----- Associate the field with the structure element -----
            // Use FormElement.Tag to link the field (widget annotation) with the /Form element.
            formStruct.Tag(txtField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}
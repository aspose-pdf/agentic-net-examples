using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.Forms; // required for form field classes

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_form_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Set basic tagging information
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Form with structure element");

            // Create a text box form field on the first page
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField textField = new TextBoxField(doc.Pages[1], fieldRect);
            textField.PartialName = "SampleText";
            textField.Value = "Enter text here";

            // Register the field in the AcroForm
            doc.Form.Add(textField);

            // Create a /Form structure element and link it directly to the field
            var root = tagged.RootElement; // use var (or dynamic) to avoid compile‑time dependency on StructureElement
            var formStruct = tagged.CreateFormElement(); // /Form element
            formStruct.Tag(textField); // associate the field (widget) with the structure element
            root.AppendChild(formStruct); // attach to the root of the structure tree

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with form saved to '{outputPath}'.");
    }
}

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

        // Load the PDF document (using the required lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // ----- Create and register a form field -----
            // Define the field rectangle (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box field associated with the document
            TextBoxField txtField = new TextBoxField(doc, fieldRect);
            txtField.Name = "SampleTextBox";
            txtField.Value = "Enter text here";

            // Add the field to the AcroForm on page 1 (pages are 1‑based)
            doc.Form.Add(txtField, 1);

            // ----- Associate the field with a /Form structure element -----
            // Access the tagged‑content API (no cast, direct assignment)
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Create a Form structure element (widget annotation) and set its alternate text
            FormElement formStruct = tagged.CreateFormElement();
            formStruct.AlternativeText = "Sample text box field";

            // Append the structure element to the root of the structure tree
            root.AppendChild(formStruct);

            // Save the modified PDF (using the required lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}
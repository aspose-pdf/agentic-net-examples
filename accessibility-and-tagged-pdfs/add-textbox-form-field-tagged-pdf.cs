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

        // Create a simple PDF if the input does not exist
        if (!File.Exists(inputPath))
        {
            using (Document tmpDoc = new Document())
            {
                tmpDoc.Pages.Add();
                tmpDoc.Save(inputPath);
            }
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure at least one page exists
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // -------------------------------------------------------------
            // 1. Create a text box form field and add it to the AcroForm
            // -------------------------------------------------------------
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField textField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                PartialName = "SampleTextField",
                Value = "Enter text here"
            };
            // The AcroForm collection is exposed via Document.Form
            doc.Form.Add(textField);

            // -------------------------------------------------------------
            // 2. Create a /Form structure element in the tagged content tree
            // -------------------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with Form Field");

            var root = tagged.RootElement; // root of the structure tree

            FormElement formStruct = tagged.CreateFormElement();
            formStruct.AlternativeText = "User input form field";
            root.AppendChild(formStruct);

            // -------------------------------------------------------------
            // 3. Associate the form field with the /Form structure element
            // -------------------------------------------------------------
            // Use the Tag method of FormElement to link the field
            formStruct.Tag(textField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}

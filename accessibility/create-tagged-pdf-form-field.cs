using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // needed for FontRepository and DefaultAppearance

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Ensure the document has a TaggedContent tree (create if missing)
            // ------------------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the tagged structure tree
            var root = tagged.RootElement;

            // ------------------------------------------------------------
            // 2. Create a new /Form structure element and attach it to the tree
            // ------------------------------------------------------------
            FormElement formStruct = tagged.CreateFormElement();
            formStruct.AlternativeText = "User input form";
            root.AppendChild(formStruct);

            // ------------------------------------------------------------
            // 3. Create an interactive text box field and register it in the AcroForm
            // ------------------------------------------------------------
            // Define the field rectangle (llx, lly, urx, ury) – coordinates are in points.
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Get the first page (or create one if the document is empty)
            Page page = doc.Pages[1];

            // Create the text box field – the constructor expects the page and rectangle.
            TextBoxField txtField = new TextBoxField(page, fieldRect);
            txtField.PartialName = "UserName";
            txtField.Value = "Enter name";

            // ----- Decoration (background, border colour) -----
            txtField.Characteristics.Background = System.Drawing.Color.LightGray; // background colour
            txtField.Characteristics.Border = System.Drawing.Color.Red;            // border colour (optional, just an example)

            // ----- Border width and style -----
            txtField.Border = new Border(txtField)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };

            // ----- Font, size and text colour -----
            // DefaultAppearance sets the visual style of the field's text.
            txtField.DefaultAppearance = new DefaultAppearance(
                FontRepository.FindFont("Helvetica"), // font name
                12,                                   // font size
                System.Drawing.Color.Black);          // text colour (System.Drawing.Color is required)

            // Register the field in the document's AcroForm collection.
            doc.Form.Add(txtField);

            // Also add the field as an annotation on the page so it becomes visible.
            page.Annotations.Add(txtField);

            // ------------------------------------------------------------
            // 4. Link the form field to the /Form structure element
            // ------------------------------------------------------------
            formStruct.Tag(txtField);

            // ------------------------------------------------------------
            // 5. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with form saved to '{outputPath}'.");
    }
}

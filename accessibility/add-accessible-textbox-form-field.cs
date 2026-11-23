using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;   // DefaultAppearance, Border, BorderStyle, AnnotationFlags
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

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
            // Set basic tagged‑PDF properties
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the structure tree
            var root = tagged.RootElement;

            // -----------------------------------------------------------------
            // 1. Create a text box form field on the first page
            // -----------------------------------------------------------------
            // Use fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField textField = new TextBoxField(doc.Pages[1], fieldRect);
            textField.PartialName = "SampleField";
            textField.Value = "Default text";

            // Appearance – font name, size and colour are set via DefaultAppearance
            // DefaultAppearance expects a System.Drawing.Color, so we qualify it explicitly
            textField.DefaultAppearance = new DefaultAppearance(
                "Helvetica",               // font name (system font or a font from FontRepository)
                12,                         // font size
                System.Drawing.Color.Black); // text colour

            // Optional visual tweaks (background / border colours)
            // Characteristics properties also use System.Drawing.Color
            textField.Characteristics.Background = System.Drawing.Color.LightYellow;
            textField.Characteristics.Border = System.Drawing.Color.Gray;
            textField.Border = new Border(textField) { Width = 1, Style = BorderStyle.Solid };

            // Register the field in the AcroForm (the document's Form collection)
            doc.Form.Add(textField);

            // -----------------------------------------------------------------
            // 2. Create a /Form structure element and attach it to the tagged tree
            // -----------------------------------------------------------------
            FormElement formStruct = tagged.CreateFormElement();
            formStruct.AlternativeText = "Sample text field for accessibility";
            root.AppendChild(formStruct);

            // Associate the form field with the structure element
            formStruct.Tag(textField);

            // -----------------------------------------------------------------
            // 3. Save the modified PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with form saved to '{outputPath}'.");
    }
}

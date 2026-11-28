using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for TextBoxField and other field types

// Alias the Facade Form to avoid ambiguity with Aspose.Pdf.Forms.Form
using FacadeForm = Aspose.Pdf.Facades.Form;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "fonts.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (needed to read font information)
        Document pdfDocument = new Document(pdfPath);

        // Use the Form facade to obtain the list of field names
        using (FacadeForm formFacade = new FacadeForm(pdfDocument))
        {
            string[] fieldNames = formFacade.FieldNames; // property, not a method

            // Prepare XML document
            XDocument doc = new XDocument(new XElement("Fonts"));

            foreach (string fieldName in fieldNames)
            {
                // Find the corresponding field object in the document model
                var field = pdfDocument.Form?.Fields?.FirstOrDefault(f => f.PartialName == fieldName);
                if (field == null)
                    continue; // No matching field found

                // Only TextBoxField (and its descendants) expose DefaultAppearance with font info
                var textBox = field as TextBoxField;
                if (textBox?.DefaultAppearance == null)
                    continue; // No appearance information – skip

                var appearance = textBox.DefaultAppearance;

                // Extract font name, size and color from DefaultAppearance
                string fontName = appearance.FontName ?? string.Empty;
                double fontSize = appearance.FontSize;
                string colorString = string.Empty;
                if (appearance.TextColor != null)
                {
                    // TextColor is System.Drawing.Color
                    var c = appearance.TextColor;
                    colorString = $"{c.R},{c.G},{c.B}";
                }

                // Build XML element for this field
                XElement fieldElement = new XElement("Field",
                    new XAttribute("Name", fieldName),
                    new XElement("FontName", fontName),
                    new XElement("FontSize", fontSize),
                    new XElement("FontColor", colorString)
                );

                doc.Root?.Add(fieldElement);
            }

            // Save the XML file
            doc.Save(xmlPath);
            Console.WriteLine($"Font details exported to '{xmlPath}'.");
        }
    }
}

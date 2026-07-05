using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Border class

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "branded_output.pdf";
        const string buttonName = "BrandButton";
        const string imagePath = "brand_logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the button field; create it if it does not exist.
            ButtonField button = doc.Form[buttonName] as ButtonField;
            if (button == null)
            {
                // Rectangle constructor expects float values.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
                button = new ButtonField(doc, rect);
                button.PartialName = buttonName;
                doc.Form.Add(button);
            }

            // Load the image using System.Drawing and assign it to the button's normal appearance.
            using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(imagePath))
            {
                button.AddImage(sysImg);
            }

            // Optional visual styling for branding consistency.
            // Border requires the parent annotation (the button) in its constructor.
            button.Border = new Border(button) { Width = 1 };
            button.Color = Aspose.Pdf.Color.LightGray;

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPdf}'.");
    }
}

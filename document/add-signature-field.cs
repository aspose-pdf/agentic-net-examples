using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature field (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create the signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            sigField.PartialName = "UserSignature";
            sigField.AlternateName = "Signature";

            // Set default appearance to match the document style (font, size, color)
            // DefaultAppearance expects a System.Drawing.Color, not Aspose.Pdf.Color
            sigField.DefaultAppearance = new DefaultAppearance("Helvetica", 10, System.Drawing.Color.Black);

            // Set border and border color (border color is set via the field's Color property)
            sigField.Color = Aspose.Pdf.Color.Gray;
            sigField.Border = new Border(sigField) { Width = 1 };

            // Add the field to the document's form collection
            doc.Form.Add(sigField);

            // Add a visual appearance for the field on page 1
            doc.Form.AddFieldAppearance(sigField, 1, sigRect);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}

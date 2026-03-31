using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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
            // Locate the signature field named "Signature"
            SignatureField signatureField = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sig && (sig.Name == "Signature" || sig.FullName == "Signature"))
                {
                    signatureField = sig;
                    break;
                }
            }

            if (signatureField == null)
            {
                Console.Error.WriteLine("Signature field 'Signature' not found.");
                return;
            }

            // Define the new rectangle in the lower‑right corner of page 5
            Page targetPage = doc.Pages[5];
            double pageWidth = targetPage.PageInfo.Width;
            double pageHeight = targetPage.PageInfo.Height;
            double rectWidth = 150; // exact width
            double rectHeight = 50; // exact height
            double marginRight = 20; // distance from right edge
            double marginBottom = 20; // distance from bottom edge
            double llx = pageWidth - rectWidth - marginRight;
            double lly = marginBottom;
            double urx = pageWidth - marginRight;
            double ury = lly + rectHeight;

            Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Update the field's rectangle
            signatureField.Rect = newRect;

            // Ensure the appearance is placed on page 5 at the new location
            doc.Form.AddFieldAppearance(signatureField, 5, newRect);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field moved and saved to '{outputPath}'.");
    }
}
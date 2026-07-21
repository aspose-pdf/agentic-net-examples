using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string imagePath = "brand.png";
        const string outputPath = "output.pdf";
        const string buttonFieldName = "BrandButton";

        if (!File.Exists(pdfPath) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Required input files are missing.");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the button field directly as ButtonField
            ButtonField button = doc.Form[buttonFieldName] as ButtonField;
            if (button == null)
            {
                Console.Error.WriteLine($"Button field '{buttonFieldName}' not found or is not a ButtonField.");
                return;
            }

            // Assign image using ButtonField.AddImage (expects System.Drawing.Image)
            using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(imagePath))
            {
                button.AddImage(sysImg);
            }

            // Assign image for XFA forms (if the PDF contains XFA)
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                doc.Form.XFA.SetFieldImage(buttonFieldName, imgStream);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Branding applied and saved to '{outputPath}'.");
    }
}

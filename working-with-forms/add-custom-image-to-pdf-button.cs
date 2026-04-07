using System;
using System.IO;
using System.Drawing; // System.Drawing.Image is required for ButtonField.AddImage
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string buttonName = "BrandButton";   // full name of the button field
        const string imagePath  = "brand.png";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Input PDF or image file not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains a form
            if (doc.Form == null)
            {
                Console.Error.WriteLine("The PDF does not contain any form fields.");
                return;
            }

            // Retrieve the button field by name
            ButtonField button = doc.Form[buttonName] as ButtonField;
            if (button == null)
            {
                Console.Error.WriteLine($"Button field '{buttonName}' not found.");
                return;
            }

            // -----------------------------------------------------------------
            // Approach 1: Use ButtonField.AddImage (core API)
            // -----------------------------------------------------------------
            // ButtonField.AddImage expects a System.Drawing.Image, not Aspose.Pdf.Image.
            // Load the image with System.Drawing.Image.FromFile and pass it directly.
            System.Drawing.Image sysImg = System.Drawing.Image.FromFile(imagePath);
            button.AddImage(sysImg);

            // -----------------------------------------------------------------
            // Approach 2: Use XFA.SetFieldImage for XFA‑based forms
            // -----------------------------------------------------------------
            // This method sets the appearance stream directly from a stream.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                doc.Form.XFA.SetFieldImage(buttonName, imgStream);
            }

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branding applied. Output saved to '{outputPdf}'.");
    }
}

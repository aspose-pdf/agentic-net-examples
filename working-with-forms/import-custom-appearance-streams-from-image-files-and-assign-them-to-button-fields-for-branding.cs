using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF with button fields
        const string outputPdf = "output_branded.pdf"; // result PDF
        const string imagePath = "brand_logo.png";     // image to use as appearance

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Input PDF or image file not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Set appearance for an AcroForm button field
            // -------------------------------------------------
            // Assume a button field named "BrandButton" already exists.
            // Retrieve it from the form collection and cast to ButtonField.
            if (doc.Form["BrandButton"] is ButtonField acroButton)
            {
                // Load the image as System.Drawing.Image (fully qualified to avoid ambiguity)
                using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(imagePath))
                {
                    acroButton.AddImage(sysImg);
                }
            }

            // -------------------------------------------------
            // 2. Set appearance for an XFA button field (if present)
            // -------------------------------------------------
            // XFA forms are accessed via the XFA property of the Form object.
            // The SetFieldImage method expects a stream containing the image.
            if (doc.Form.XFA != null)
            {
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    // Replace "XFAButton" with the actual XFA field name.
                    doc.Form.XFA.SetFieldImage("XFAButton", imgStream);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPdf}'.");
    }
}

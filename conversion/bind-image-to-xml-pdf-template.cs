using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlTemplatePath = "template.xml";
        const string imagePath        = "image.jpg";
        const string outputPdfPath    = "result.pdf";

        if (!File.Exists(xmlTemplatePath))
        {
            Console.Error.WriteLine($"Template not found: {xmlTemplatePath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load the XML template as a PDF document
        using (Document doc = new Document(xmlTemplatePath, new XmlLoadOptions()))
        {
            // Id of the <Image> element defined in the XML template
            const string imageFieldId = "myImage"; // replace with actual Id

            // Locate the image field in the form and set its source file
            if (doc.Form.HasField(imageFieldId))
            {
                // Use dynamic to avoid compile‑time dependency on a specific field type.
                // The underlying field (ImageField, FileField, etc.) exposes a "File" property.
                dynamic imgField = doc.Form[imageFieldId];
                try
                {
                    imgField.File = imagePath;
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    Console.Error.WriteLine($"Field '{imageFieldId}' does not support a File property.");
                }
            }
            else
            {
                Console.Error.WriteLine($"Image field with Id '{imageFieldId}' not found in template.");
            }

            // Save the bound PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF with a form field
        const string outputPdfPath = "output.pdf";         // PDF with background image applied
        const string fieldName     = "BrandField";         // name of the form field to style
        const string imagePath     = "brand_logo.png";    // image to use as background

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document contains a form
            if (doc.Form == null)
            {
                Console.Error.WriteLine("The PDF does not contain a form.");
                return;
            }

            // Set the background image for the specified XFA field.
            // XFA.SetFieldImage expects a field name and a Stream containing the image.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                doc.Form.XFA.SetFieldImage(fieldName, imgStream);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background image applied to field '{fieldName}' and saved to '{outputPdfPath}'.");
    }
}
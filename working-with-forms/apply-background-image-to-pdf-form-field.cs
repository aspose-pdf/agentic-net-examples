using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with a form field
        const string outputPdf = "output.pdf";         // PDF after applying background image
        const string fieldName = "BrandField";         // name of the form field to style
        const string imagePath = "brand.png";          // background image file

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains a form
            if (doc.Form == null)
            {
                Console.Error.WriteLine("The PDF does not contain a form.");
                return;
            }

            // XFA forms support setting a background image directly on a field
            // The SetFieldImage method expects the field name and a stream containing the image
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Apply the image as the background of the specified field
                doc.Form.XFA.SetFieldImage(fieldName, imgStream);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background image applied to field '{fieldName}' and saved as '{outputPdf}'.");
    }
}
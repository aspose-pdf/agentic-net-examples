using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputPdf = "form_with_background.pdf";
        const string fieldName = "BrandButton"; // Fully qualified name of the target form field
        const string imagePath = "brand.png";

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

        // Load the PDF form using the Facades Form class
        using (Form form = new Form(inputPdf))
        {
            // Apply the background image to the specified button field.
            // This replaces the button's appearance with the provided image.
            form.FillImageField(fieldName, imagePath);

            // Save the updated PDF to a new file.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Background image applied to field '{fieldName}' and saved as '{outputPdf}'.");
    }
}
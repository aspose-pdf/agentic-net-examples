using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "background.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Bind the PDF for editing/signature operations
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Set the image that will be used as the signature appearance background
            pdfSign.SignatureAppearance = imagePath;

            // Locate the signature field named "Signature"
            // The Form indexer returns a WidgetAnnotation, so we need an explicit cast to SignatureField
            SignatureField sigField = pdfSign.Document.Form["Signature"] as SignatureField;
            if (sigField != null)
            {
                // Create a custom appearance object
                SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
                {
                    BackgroundColor = Aspose.Pdf.Color.Transparent, // transparent background
                    IsForegroundImage = false // draw the image as background
                };

                // Assign the custom appearance to the signature field
                sigField.Signature.CustomAppearance = customAppearance;
            }
            else
            {
                Console.Error.WriteLine("Signature field named \"Signature\" not found.");
            }

            // Save the modified PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}

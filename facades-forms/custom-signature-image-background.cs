using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the background image and the output PDF
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string backgroundImage = "background.jpg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(backgroundImage))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImage}");
            return;
        }

        // Use PdfFileSignature (Facade) to work with the PDF document
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the existing PDF
            pdfSignature.BindPdf(inputPdf);

            // Set the image that will be used as the visual appearance of the signature
            // This image will be drawn as the background of the signature field
            pdfSignature.SignatureAppearance = backgroundImage;

            // Access the underlying Document to locate the signature field named "Signature"
            Document doc = pdfSignature.Document;

            SignatureField signatureField = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sig && sig.PartialName == "Signature")
                {
                    signatureField = sig;
                    break;
                }
            }

            if (signatureField == null)
            {
                Console.Error.WriteLine("Signature field named 'Signature' not found.");
                return;
            }

            // Create a custom appearance object
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();

            // Set the image to be drawn as a background (not foreground)
            customAppearance.IsForegroundImage = false;

            // Assign the custom appearance to the signature field
            signatureField.Signature.CustomAppearance = customAppearance;

            // Save the modified PDF
            pdfSignature.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom signature appearance: {outputPdf}");
    }
}
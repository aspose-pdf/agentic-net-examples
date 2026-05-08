using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the image to be used as the signature appearance
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg  = "signature_stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will be placed
            // (coordinates are in points; adjust as needed)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            sigField.Name = "ClientSignature";          // Set the field name
            doc.Form.Add(sigField);                     // Add the field to the form

            // Create an image stamp that will serve as the visual appearance
            ImageStamp imgStamp = new ImageStamp(stampImg);
            imgStamp.XIndent = rect.LLX;                // Align stamp with the field rectangle
            imgStamp.YIndent = rect.LLY;
            imgStamp.Width   = rect.URX - rect.LLX;
            imgStamp.Height  = rect.URY - rect.LLY;
            imgStamp.Background = false;                // Draw on top of page content

            // Add the stamp to the same page (visual representation of the signature)
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signature field 'ClientSignature' added with image appearance. Saved to '{outputPdf}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImage = "signature_stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will be placed
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create a signature field, assign a name, and add it to the form
            SignatureField sigField = new SignatureField(doc, sigRect);
            sigField.Name = "ClientSignature";
            doc.Form.Add(sigField);

            // Create an image stamp from the predefined image
            ImageStamp imgStamp = new ImageStamp(stampImage);
            // Align the stamp with the signature field rectangle
            imgStamp.XIndent = sigRect.LLX;
            imgStamp.YIndent = sigRect.LLY;
            imgStamp.Width = sigRect.URX - sigRect.LLX;
            imgStamp.Height = sigRect.URY - sigRect.LLY;
            imgStamp.Background = false; // draw on top of page content

            // Add the stamp to the page that contains the signature field (page 1 in this example)
            doc.Pages[1].AddStamp(imgStamp);

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signature field 'ClientSignature' with image appearance saved to '{outputPdf}'.");
    }
}
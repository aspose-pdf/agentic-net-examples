using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputPdfPath = "signed_output.pdf"; // result PDF
        const string signatureImg = "signature.png";      // custom signature image

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(signatureImg))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImg}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the last page (Aspose.Pdf uses 1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Define the rectangle where the signature will appear
            // Rectangle(left, bottom, right, top) – use double literals to match the constructor signature
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100.0, 100.0, 300.0, 200.0);

            // Create a signature field on the last page
            SignatureField sigField = new SignatureField(lastPage, sigRect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // OPTIONAL: Place the same image as a visual stamp so the user sees it
            // (ImageStamp works with Page.AddStamp)
            ImageStamp imgStamp = new ImageStamp(signatureImg)
            {
                // Position the stamp exactly where the signature field is defined
                XIndent = sigRect.LLX,
                YIndent = sigRect.LLY,
                Width = sigRect.URX - sigRect.LLX,
                Height = sigRect.URY - sigRect.LLY,
                // Foreground (false = top of page content)
                Background = false,
                Opacity = 1.0f
            };
            lastPage.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signature appearance added and saved to '{outputPdfPath}'.");
    }
}

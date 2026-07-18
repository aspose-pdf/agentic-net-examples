using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "signed_output.pdf"; // result PDF
        const string signatureImage = "signature.png";     // custom image for appearance

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(signatureImage))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImage}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the last page (Aspose.Pdf uses 1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Define the rectangle where the signature field and its appearance will be placed
            // (coordinates are in points; adjust as needed)
            double llx = 100; // lower‑left X
            double lly = 100; // lower‑left Y
            double urx = 300; // upper‑right X
            double ury = 200; // upper‑right Y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a signature field on the last page
            SignatureField sigField = new SignatureField(lastPage, rect)
            {
                PartialName = "Signature1" // optional field name
            };
            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Create an image stamp using the custom signature image
            ImageStamp imgStamp = new ImageStamp(signatureImage)
            {
                // Position the stamp to match the signature field rectangle
                XIndent = llx,
                YIndent = lly,
                Width   = urx - llx,
                Height  = ury - lly,
                // Ensure the stamp is drawn on top of page content
                Background = false
            };

            // Add the image stamp to the last page
            lastPage.AddStamp(imgStamp);

            // Save the modified PDF (lifecycle rule: use the provided save method)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signature appearance added and saved to '{outputPdfPath}'.");
    }
}
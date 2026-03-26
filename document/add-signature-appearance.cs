using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // Added for SignatureField

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string imagePath = "signature.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Signature image not found: {imagePath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Get the last page (1‑based indexing)
            int lastPageNumber = doc.Pages.Count;
            Page lastPage = doc.Pages[lastPageNumber];

            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the last page
            SignatureField sigField = new SignatureField(lastPage, rect);
            doc.Form.Add(sigField);

            // Create an image stamp with the custom signature image
            ImageStamp imgStamp = new ImageStamp(imagePath);
            imgStamp.XIndent = (float)rect.LLX;
            imgStamp.YIndent = (float)rect.LLY;
            imgStamp.Width = (float)(rect.URX - rect.LLX);
            imgStamp.Height = (float)(rect.URY - rect.LLY);
            imgStamp.Background = false; // place on top of content

            // Add the image stamp to the same location on the last page
            lastPage.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature with custom image added. Saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_branded.pdf";
        const string buttonName = "BrandButton"; // exact name of the button field in the PDF
        const string imagePath = "brand_logo.png";

        // Ensure files exist before proceeding.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Approach: Set image for AcroForm button fields.
            // ------------------------------------------------------------
            // Retrieve the button field from the form collection and cast it safely.
            ButtonField button = pdfDoc.Form[buttonName] as ButtonField;
            if (button != null)
            {
                // Fully qualify System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Image.
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                {
                    button.AddImage(img);
                }
            }
            else
            {
                Console.WriteLine($"Field '{buttonName}' is not a ButtonField or does not exist.");
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPdfPath}'.");
    }
}

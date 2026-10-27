using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string outputImage = "signature_appearance.png";
        const string signatureFieldName = "Signature1";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Locate the signature field by its partial name
            SignatureField sigField = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField s && s.PartialName == signatureFieldName)
                {
                    sigField = s;
                    break;
                }
            }

            if (sigField == null)
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
                return;
            }

            // Get the rectangle that defines the visual area of the field
            Aspose.Pdf.Rectangle fieldRect = sigField.Rect;

            // The field resides on a specific page (PageIndex is 1‑based)
            int pageNumber = sigField.PageIndex;
            Page page = doc.Pages[pageNumber];

            // Preserve the original CropBox so we can restore it later
            Aspose.Pdf.Rectangle originalCropBox = page.CropBox;

            // Temporarily set the page's CropBox to the signature rectangle
            page.CropBox = fieldRect;

            // Configure a PNG device with a suitable resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Use the CropBox coordinates for rendering
                CoordinateType = PageCoordinateType.CropBox
            };

            // Render only the cropped region (signature appearance) to a memory stream and then to a file
            using (MemoryStream ms = new MemoryStream())
            {
                // Process the page; the device will render the area defined by the current CropBox
                pngDevice.Process(page, ms);
                File.WriteAllBytes(outputImage, ms.ToArray());
            }

            // Restore the original CropBox
            page.CropBox = originalCropBox;

            Console.WriteLine($"Signature appearance saved to '{outputImage}'.");
        }
    }
}

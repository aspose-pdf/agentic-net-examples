using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "product.pdf";
        const string outputPdfPath  = "product_verified.pdf";
        const string qrImagePath    = "qr_code.png";
        const string verificationText = "Verify product authenticity";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR code image not found: {qrImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp for the QR code
            ImageStamp qrStamp = new ImageStamp(qrImagePath)
            {
                // Position the QR code at the bottom‑right corner of the first page
                // (coordinates are in points; 0,0 is lower‑left)
                // Adjust as needed for your layout
                BottomMargin = 20,
                RightMargin  = 20,
                // Set the stamp size (optional)
                Width  = 100,
                Height = 100,
                // Ensure the stamp is placed on top of existing content
                Background = false
            };

            // Create a TextStamp for the descriptive text
            TextStamp textStamp = new TextStamp(verificationText)
            {
                // Position the text just above the QR code
                BottomMargin = qrStamp.BottomMargin + qrStamp.Height + 10,
                RightMargin  = qrStamp.RightMargin,
                // Set the text appearance
                // TextState is read‑only; modify its properties instead of assigning a new instance
                // Use fully qualified type names to avoid ambiguity
                // Font size, color, and other properties can be customized here
                // Example: 24‑point bold font, dark gray color
                // Note: Font must be available; using built‑in Helvetica
                // The Font property expects a Font object from Aspose.Pdf.Text
                // The Color property expects Aspose.Pdf.Color
                // No System.Drawing usage
                // Set alignment if needed (centered, left, etc.)
                // Here we keep default left alignment
                // The stamp will be rendered on the page content
                // (Background = false means it appears over the page)
                Background = false
            };
            // Modify the existing TextState object
            textStamp.TextState.FontSize = 24;
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            textStamp.TextState.FontStyle = FontStyles.Bold;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;

            // Apply both stamps to the first page
            // PageCollection does not have AddStamp; stamps are added per Page
            Page firstPage = pdfDoc.Pages[1];
            firstPage.AddStamp(qrStamp);
            firstPage.AddStamp(textStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
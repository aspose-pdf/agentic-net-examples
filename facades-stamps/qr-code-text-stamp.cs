using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string qrImagePath = "qr.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine("QR code image not found: " + qrImagePath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // QR code image stamp
            ImageStamp imageStamp = new ImageStamp(qrImagePath)
            {
                XIndent = 50.0f,
                YIndent = 700.0f,
                Width = 100.0f,
                Height = 100.0f,
                Background = false,
                Opacity = 1.0f
            };

            // Descriptive text stamp
            TextStamp textStamp = new TextStamp("Product Verified");
            // Configure TextState directly (TextState is read‑only)
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            textStamp.TextState.FontSize = 12.0f;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            textStamp.XIndent = 50.0f;
            textStamp.YIndent = 650.0f; // positioned below the QR code
            textStamp.Background = false;
            textStamp.Opacity = 1.0f;

            // Apply stamps to the first page
            Page firstPage = document.Pages[1];
            firstPage.AddStamp(imageStamp);
            firstPage.AddStamp(textStamp);

            document.Save(outputPath);
        }

        Console.WriteLine("Stamps added and saved to '" + outputPath + "'.");
    }
}

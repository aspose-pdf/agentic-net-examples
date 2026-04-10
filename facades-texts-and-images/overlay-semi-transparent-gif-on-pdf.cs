using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the semi‑transparent GIF and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string gifPath       = "overlay.gif";

        // Coordinates where the GIF will be placed (lower‑left and upper‑right corners)
        // Adjust these values to match the desired position and size on the page.
        float lowerLeftX  = 100f;
        float lowerLeftY  = 500f;
        float upperRightX = 300f;
        float upperRightY = 700f;

        // Verify that required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(gifPath))
        {
            Console.Error.WriteLine("Input PDF or GIF file not found.");
            return;
        }

        // Create compositing parameters. BlendMode.Normal preserves the GIF's own transparency.
        CompositingParameters compParams = new CompositingParameters(BlendMode.Normal);

        // Use PdfFileMend (a Facade) to overlay the GIF onto the existing PDF page.
        using (PdfFileMend mender = new PdfFileMend())
        {
            // Bind the source PDF document.
            mender.BindPdf(inputPdfPath);

            // Add the GIF image to page 1 using the compositing parameters.
            using (FileStream gifStream = File.OpenRead(gifPath))
            {
                mender.AddImage(gifStream, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY, compParams);
            }

            // Save the modified PDF to the output path.
            mender.Save(outputPdfPath);
            mender.Close();
        }

        Console.WriteLine($"Overlay completed. Output saved to '{outputPdfPath}'.");
    }
}
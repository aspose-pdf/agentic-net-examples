using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string gifPath = "overlay.gif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }
        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine("Overlay GIF not found: " + gifPath);
            return;
        }

        // Initialize PdfFileMend with source and destination files
        PdfFileMend mender = new PdfFileMend(inputPdf, outputPdf);

        // Create compositing parameters that preserve the GIF's transparency
        CompositingParameters compParams = new CompositingParameters(BlendMode.Normal);

        // Add the semi‑transparent GIF onto page 1 at the desired location
        using (FileStream gifStream = File.OpenRead(gifPath))
        {
            // lower‑left (50, 400), upper‑right (250, 600) – adjust as needed
            bool success = mender.AddImage(gifStream, 1, 50f, 400f, 250f, 600f, compParams);
            if (!success)
            {
                Console.Error.WriteLine("Failed to add overlay image.");
            }
        }

        mender.Close();
        Console.WriteLine("Overlay completed. Output saved to " + outputPdf);
    }
}

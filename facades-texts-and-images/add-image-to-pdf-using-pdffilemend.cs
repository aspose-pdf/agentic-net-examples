using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "logo.png";

        // Verify required files exist
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!System.IO.File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create the PdfFileMend facade
        PdfFileMend mend = new PdfFileMend();

        try
        {
            // Bind the source PDF document
            mend.BindPdf(inputPdf);

            // Example operation: add an image to page 1
            // Parameters: image file path, page number, lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            mend.AddImage(imagePath, 1, 100f, 500f, 300f, 700f);

            // Save the modified PDF
            mend.Save(outputPdf);
        }
        finally
        {
            // Ensure the facade is closed and resources are released
            mend.Close();
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}
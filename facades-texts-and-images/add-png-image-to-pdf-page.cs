using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

        // Verify that source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Use a using‑statement to ensure the facade is disposed correctly.
        // This also eliminates the need for an explicit Close() call and avoids
        // potential file‑handle conflicts that can trigger the package assets
        // cache I/O warning when builds run in parallel.
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the PDF document to be edited
            mend.BindPdf(inputPdf);

            // Add the PNG image to page 2.
            // Coordinates are: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            // Adjust these values as needed for the desired placement.
            mend.AddImage(imagePath, 2, 100f, 200f, 300f, 400f);

            // Save the modified PDF to a new file
            mend.Save(outputPdf);
        }
    }
}

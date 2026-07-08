using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates how to add an image to an existing PDF document using Aspose.Pdf.Facades.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "stamp.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Use a using block for deterministic disposal of the PDF document.
        using (Document doc = new Document(inputPdfPath))
        {
            // PdfFileMend provides AddImage methods for inserting images.
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(doc);

            // Add the image to page 1.
            // Parameters: image file path, page number (1‑based), lower‑left X, lower‑left Y,
            // upper‑right X, upper‑right Y (coordinates are in points, 1 inch = 72 points).
            mend.AddImage(imagePath, 1, 100, 500, 300, 700);

            // Save the modified document.
            mend.Save(outputPdfPath);
            mend.Close();
        }

        Console.WriteLine($"Image added successfully. Output saved to '{outputPdfPath}'.");
    }
}
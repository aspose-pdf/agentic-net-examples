using System;
using System.IO;
using Aspose.Pdf; // Document and Rotation enum are defined here

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where rotated PDFs will be saved
        const string outputFolder = "RotatedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string outPath = Path.Combine(outputFolder, fileName);

            // Load the PDF document
            Document pdfDoc = new Document(pdfPath);

            // Rotate the first page by 90 degrees clockwise (if the document has at least one page)
            if (pdfDoc.Pages.Count > 0)
            {
                pdfDoc.Pages[1].Rotate = Rotation.on90; // Rotation enum lives in Aspose.Pdf
            }

            // Save the modified PDF to the output location
            pdfDoc.Save(outPath);
        }

        Console.WriteLine("All PDFs have been processed and first pages rotated.");
    }
}

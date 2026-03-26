using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        string inputFolder = "pdfs";   // folder containing source PDFs
        string outputFolder = "tiffs"; // folder for generated TIFFs

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string tiffPath = Path.Combine(outputFolder, fileName + ".tiff");

            using (Document pdfDoc = new Document(pdfPath))
            using (FileStream tiffStream = new FileStream(tiffPath, FileMode.Create))
            {
                // TiffDevice uses default compression when no options are set
                TiffDevice tiffDevice = new TiffDevice();
                tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, tiffStream);
            }

            Console.WriteLine($"Converted '{pdfPath}' to '{tiffPath}'.");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Added namespace for PdfFileInfo

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Ensure the PDF file exists – create a minimal one if it does not.
        if (!File.Exists(inputPath))
        {
            var doc = new Document();
            doc.Info.Creator = "GeneratedByProgram"; // set a known creator value
            doc.Pages.Add(); // add a blank page so the file is valid
            doc.Save(inputPath);
        }

        // Use PdfFileInfo (from Aspose.Pdf.Facades) to read the Creator metadata without loading the whole document.
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);
        string creator = pdfInfo.Creator;

        // Example usage: output the value
        Console.WriteLine($"Creator: {creator}");
    }
}

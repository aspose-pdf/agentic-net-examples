using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Generate a new GUID to use as the document identifier
        string documentId = Guid.NewGuid().ToString();

        // Create the PdfFileInfo facade, bind the source PDF, set the ID, and save
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Load the PDF file into the facade
            pdfInfo.BindPdf(inputPath);

            // Store the GUID as a custom metadata entry named "DocumentID"
            pdfInfo.SetMetaInfo("DocumentID", documentId);

            // Save the updated PDF to a new file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"PDF saved with DocumentID = {documentId}");
    }
}
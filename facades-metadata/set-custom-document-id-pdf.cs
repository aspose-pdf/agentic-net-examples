using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_id.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Generate a new GUID to be used as the document ID
        string documentId = Guid.NewGuid().ToString();

        // Use PdfFileInfo facade to modify the PDF metadata
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Store the GUID as a custom meta information field named "DocumentID"
            pdfInfo.SetMetaInfo("DocumentID", documentId);

            // Save the updated PDF to a new file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Document ID set to {documentId}");
        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
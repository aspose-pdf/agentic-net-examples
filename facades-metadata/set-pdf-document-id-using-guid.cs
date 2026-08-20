using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_guid.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Generate a new GUID to be used as the document identifier
        Guid documentGuid = Guid.NewGuid();

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize the PdfFileInfo facade and bind it to the loaded document
            using (PdfFileInfo fileInfo = new PdfFileInfo())
            {
                fileInfo.BindPdf(pdfDoc);

                // Store the GUID as a custom metadata entry (e.g., "DocumentID")
                fileInfo.SetMetaInfo("DocumentID", documentGuid.ToString());

                // Save the updated PDF with the new metadata
                fileInfo.SaveNewInfo(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with GUID {documentGuid} as DocumentID to '{outputPath}'.");
    }
}
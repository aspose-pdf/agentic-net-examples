using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document with a single blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Save the initial document
            doc.Save(outputPath);
        }

        // Generate a new GUID
        Guid documentGuid = Guid.NewGuid();

        // Bind the saved PDF and set the GUID as custom metadata "DocumentID"
        using (PdfFileInfo fileInfo = new PdfFileInfo(outputPath))
        {
            fileInfo.SetMetaInfo("DocumentID", documentGuid.ToString());
            // Save the updated PDF (overwrites the original file)
            fileInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine("PDF created with DocumentID: " + documentGuid);
    }
}
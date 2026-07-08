using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo provides access to custom metadata via GetMetaInfo
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Attempt to read the custom metadata key "Confidential"
            string confidentialValue = pdfInfo.GetMetaInfo("Confidential");

            // GetMetaInfo returns an empty string if the key does not exist
            if (string.IsNullOrEmpty(confidentialValue))
            {
                Console.WriteLine("Custom metadata 'Confidential' does not exist.");
            }
            else
            {
                Console.WriteLine($"Confidential: {confidentialValue}");
            }
        }
    }
}
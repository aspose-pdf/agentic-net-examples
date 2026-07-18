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

        // PdfFileInfo is a facade for accessing PDF metadata
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the custom metadata value for the key "Confidential"
            // GetMetaInfo returns an empty string if the key does not exist
            string confidentialValue = pdfInfo.GetMetaInfo("Confidential");

            if (!string.IsNullOrEmpty(confidentialValue))
            {
                Console.WriteLine($"Confidential: {confidentialValue}");
            }
            else
            {
                Console.WriteLine("Custom metadata key \"Confidential\" not found.");
            }
        }
    }
}
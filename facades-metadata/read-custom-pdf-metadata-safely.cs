using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use PdfFileInfo facade to access custom metadata.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the value for the custom key "Confidential".
            // GetMetaInfo returns an empty string if the key does not exist.
            string confidentialValue = pdfInfo.GetMetaInfo("Confidential");

            if (!string.IsNullOrEmpty(confidentialValue))
            {
                Console.WriteLine($"Confidential metadata: {confidentialValue}");
            }
            else
            {
                Console.WriteLine("Custom metadata key 'Confidential' does not exist.");
            }
        }
    }
}
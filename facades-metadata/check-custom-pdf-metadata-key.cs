using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileInfo facade for the PDF document.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Get the value of the custom metadata key "Confidential".
            // GetMetaInfo returns an empty string if the key does not exist.
            string confidentialValue = pdfInfo.GetMetaInfo("Confidential");

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
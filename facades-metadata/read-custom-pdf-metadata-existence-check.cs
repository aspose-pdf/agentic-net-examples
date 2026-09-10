using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the file exists before proceeding
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo is a facade for accessing PDF metadata; wrap it in a using block for proper disposal
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // GetMetaInfo returns an empty string when the custom key is absent
            string confidential = pdfInfo.GetMetaInfo("Confidential");

            if (string.IsNullOrEmpty(confidential))
            {
                Console.WriteLine("Custom metadata key 'Confidential' does not exist.");
            }
            else
            {
                Console.WriteLine($"Confidential: {confidential}");
            }
        }
    }
}
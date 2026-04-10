using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use Aspose.Pdf Document to access metadata. This avoids the external AsposePdfApi.exe process.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Custom metadata is stored directly in the DocumentInfo indexer.
            // Verify that the key exists before trying to read its value.
            if (pdfDoc.Info.ContainsKey("Confidential"))
            {
                // The value is stored as a string in the DocumentInfo indexer.
                string confidentialValue = pdfDoc.Info["Confidential"];
                Console.WriteLine($"Confidential metadata: {confidentialValue}");
            }
            else
            {
                Console.WriteLine("Custom metadata key 'Confidential' not found.");
            }
        }
    }
}

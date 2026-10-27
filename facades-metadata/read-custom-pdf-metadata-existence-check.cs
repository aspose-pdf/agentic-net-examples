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

        // Load the PDF document. Document implements IDisposable, so use a using block.
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the custom metadata value for the key "Confidential" from the document's Info dictionary.
            string confidentialValue = doc.Info["Confidential"];

            if (!string.IsNullOrEmpty(confidentialValue))
            {
                Console.WriteLine($"Confidential: {confidentialValue}");
            }
            else
            {
                Console.WriteLine("Custom metadata key 'Confidential' not found.");
            }
        }
    }
}

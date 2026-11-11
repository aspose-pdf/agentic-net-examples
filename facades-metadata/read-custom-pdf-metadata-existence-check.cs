using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document (Document loads the full file but provides access to custom metadata)
        Document pdfDoc = new Document(inputPath);

        // Check for the custom metadata key "Confidential"
        if (pdfDoc.Info != null && pdfDoc.Info.ContainsKey("Confidential"))
        {
            string confidentialValue = pdfDoc.Info["Confidential"];
            Console.WriteLine($"Confidential: {confidentialValue}");
        }
        else
        {
            Console.WriteLine("Custom metadata key 'Confidential' does not exist.");
        }
    }
}
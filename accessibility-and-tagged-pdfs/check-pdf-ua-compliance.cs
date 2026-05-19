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

        // Load the PDF document within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Check PDF/UA compliance
            bool isUaCompliant = doc.IsPdfUaCompliant;

            // Log the result
            Console.WriteLine($"PDF/UA compliant: {isUaCompliant}");
        }
    }
}
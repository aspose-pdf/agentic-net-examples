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

        using (Document doc = new Document(inputPath))
        {
            bool isUaCompliant = doc.IsPdfUaCompliant;
            Console.WriteLine($"PDF/UA compliant: {isUaCompliant}");
        }
    }
}
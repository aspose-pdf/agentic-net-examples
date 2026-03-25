using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath = "ua_compliance.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                bool isUaCompliant = doc.IsPdfUaCompliant;
                Console.WriteLine($"PDF/UA compliant: {isUaCompliant}");
                File.WriteAllText(logPath, isUaCompliant.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
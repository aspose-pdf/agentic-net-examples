using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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
            // Retrieve the total number of AcroForm fields
            int fieldCount = doc.Form.Count;

            // Output the count to the console
            Console.WriteLine($"AcroForm field count: {fieldCount}");
        }
    }
}
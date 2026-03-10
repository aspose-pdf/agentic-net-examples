using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "blank.pdf";

        // Create an empty PDF document using the Document constructor
        using (var doc = new Document())
        {
            // Save the document as PDF (extension determines format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank PDF saved to '{outputPath}'.");
    }
}
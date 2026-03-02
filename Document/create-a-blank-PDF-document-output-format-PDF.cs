using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "blank.pdf";

        // Use a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a single blank page (optional, ensures the PDF has at least one page)
            doc.Pages.Add();

            // Save the document as PDF (default Save(string) writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank PDF saved to '{outputPath}'.");
    }
}
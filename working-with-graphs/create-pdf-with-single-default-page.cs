using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new empty PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single default‑size page (A4 is the default page size)
            doc.Pages.Add();

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }
}
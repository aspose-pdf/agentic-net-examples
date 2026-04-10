using System;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Create a new PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document())
        {
            // Set the creation date of the document
            doc.Info.CreationDate = DateTime.Now;

            // Set the modification date of the document
            doc.Info.ModDate = DateTime.Now;

            // Set custom keywords (comma‑separated string)
            doc.Info.Keywords = "Aspose.Pdf, example, metadata";

            // Save the PDF – without SaveOptions the file is always written as PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF created with creation date, modification date, and keywords.");
    }
}
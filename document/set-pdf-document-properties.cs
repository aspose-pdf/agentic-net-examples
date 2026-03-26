using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // Set creation and modification dates
            doc.Info.CreationDate = DateTime.Now;
            doc.Info.ModDate = DateTime.Now;

            // Set custom keywords
            doc.Info.Keywords = "Aspose.Pdf, example, metadata";

            // Optional: add additional custom data
            doc.Info.Add("CustomKey", "CustomValue");

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
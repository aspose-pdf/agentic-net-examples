using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputMobi = "output.mobi";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Set standard author metadata
            doc.Info.Author = "John Doe";

            // Add custom publisher metadata (not a predefined key, so use Add)
            doc.Info.Add("Publisher", "Acme Publishing");

            // Save the document as MOBI format using MobiXmlSaveOptions.
            // This is the correct way to generate MOBI; SaveFormat does not contain a Mobi member.
            var mobiOptions = new MobiXmlSaveOptions();
            doc.Save(outputMobi, mobiOptions);
        }

        Console.WriteLine($"PDF successfully converted to MOBI: {outputMobi}");
    }
}

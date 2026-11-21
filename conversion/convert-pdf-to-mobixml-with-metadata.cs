using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.mobi";
        const string author = "John Doe";
        const string publisher = "Acme Publishing";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Set standard author metadata
                doc.Info.Author = author;

                // Set custom publisher metadata via the Info dictionary
                doc.Info["Publisher"] = publisher;

                // Create MobiXml save options (all SaveOptions are in Aspose.Pdf namespace)
                MobiXmlSaveOptions mobiOptions = new MobiXmlSaveOptions();

                // Save the document as MobiXml
                doc.Save(outputPath, mobiOptions);
            }

            Console.WriteLine($"PDF successfully converted to MobiXml with metadata. Output: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
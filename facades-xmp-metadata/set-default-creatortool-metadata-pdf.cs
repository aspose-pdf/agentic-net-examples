using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Configuration option for default CreatorTool value
    private const string DefaultCreatorTool = "MyAppCreatorTool";

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf.Document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Check the existing Creator value via DocumentInfo (or PdfFileInfo)
            // If it is null or empty, assign the default value from configuration
            if (string.IsNullOrWhiteSpace(doc.Info.Creator))
            {
                doc.Info.Creator = DefaultCreatorTool;
            }

            // Save the modified document (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
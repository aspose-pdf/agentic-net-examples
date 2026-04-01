using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";
        string defaultCreator = "MyApp CreatorTool";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // If the Creator metadata is missing, assign the default value
            if (String.IsNullOrEmpty(document.Info.Creator))
            {
                document.Info.Creator = defaultCreator;
                Console.WriteLine("Creator set to default: " + defaultCreator);
            }
            else
            {
                Console.WriteLine("Existing Creator: " + document.Info.Creator);
            }

            document.Save(outputPath);
        }

        Console.WriteLine("Saved PDF with Creator to '" + outputPath + "'.");
    }
}
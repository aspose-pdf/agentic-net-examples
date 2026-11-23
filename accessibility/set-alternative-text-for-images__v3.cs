using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                ITaggedContent tagged = doc.TaggedContent;
                if (tagged == null)
                {
                    Console.WriteLine("Document is not tagged. No structure to modify.");
                    doc.Save(outputPath);
                    return;
                }

                StructureElement root = tagged.RootElement;
                var figures = root.FindElements<FigureElement>(true);
                int index = 1;
                foreach (FigureElement fig in figures)
                {
                    fig.AlternativeText = $"Image {index} description";
                    index++;
                }

                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
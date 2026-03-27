using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputCsv = "annotations.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                writer.WriteLine("AnnotationName,PageNumber");
                int pageCount = doc.Pages.Count;
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    AnnotationCollection annCollection = page.Annotations;
                    for (int annIndex = 1; annIndex <= annCollection.Count; annIndex++)
                    {
                        Annotation annotation = annCollection[annIndex];
                        string name = annotation.Name ?? string.Empty;
                        writer.WriteLine(name + "," + pageIndex);
                    }
                }
            }
        }

        Console.WriteLine("Annotations exported to " + outputCsv);
    }
}

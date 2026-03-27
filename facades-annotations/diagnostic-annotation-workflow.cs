using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";
        const string logPath = "annotation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (StreamWriter logger = new StreamWriter(logPath, false))
        {
            logger.WriteLine("Annotation workflow started at " + DateTime.Now);

            using (Document doc = new Document(inputPath))
            {
                logger.WriteLine("Loaded document: " + inputPath);
                logger.WriteLine("Page count: " + doc.Pages.Count);

                // Add a LinkAnnotation on the first page
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
                LinkAnnotation link = new LinkAnnotation(doc.Pages[1], linkRect);
                link.Action = new GoToURIAction("https://www.example.com");
                link.Color = Aspose.Pdf.Color.Blue;
                doc.Pages[1].Annotations.Add(link);
                logger.WriteLine("Added LinkAnnotation to page 1 with rectangle " + linkRect.ToString());

                // Add a TextAnnotation on the first page
                Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 400, 300, 450);
                TextAnnotation textAnn = new TextAnnotation(doc.Pages[1], textRect);
                textAnn.Title = "Note";
                textAnn.Contents = "This is a sample text annotation.";
                textAnn.Color = Aspose.Pdf.Color.Yellow;
                doc.Pages[1].Annotations.Add(textAnn);
                logger.WriteLine("Added TextAnnotation to page 1 with rectangle " + textRect.ToString());

                // Save the annotated document
                doc.Save(outputPath);
                logger.WriteLine("Saved annotated document as " + outputPath);
            }

            logger.WriteLine("Annotation workflow completed at " + DateTime.Now);
        }

        Console.WriteLine("Process completed. See " + logPath);
    }
}
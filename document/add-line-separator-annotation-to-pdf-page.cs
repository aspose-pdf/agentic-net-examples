using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define a zero‑size rectangle; the line geometry is defined by start/end points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Define start and end points for the separator line
            Aspose.Pdf.Point start = new Aspose.Pdf.Point(50, 500);
            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(550, 500);

            // Create the line annotation first, then set its properties separately
            LineAnnotation line = new LineAnnotation(page, rect, start, end);
            line.Color = Aspose.Pdf.Color.Gray;                     // line colour
            line.Border = new Border(line) { Width = 1 };            // line width via Border

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Rectangle coordinates: lower‑left x, lower‑left y, upper‑right x, upper‑right y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            LinkAnnotation link = new LinkAnnotation(page, rect);
            link.Action = new JavascriptAction("app.alert('Hello from Aspose.Pdf!');");
            link.Color = Aspose.Pdf.Color.Blue;

            page.Annotations.Add(link);

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript annotation saved to '{outputPath}'.");
    }
}
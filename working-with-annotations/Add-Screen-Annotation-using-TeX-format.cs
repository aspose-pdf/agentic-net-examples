using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string texFile = "sample.tex"; // Path to the TeX file used as media

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX file not found: {texFile}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Add a TeX fragment as visible content on the first page
            Page page = doc.Pages[1];
            TeXFragment texFragment = new TeXFragment(@"E = mc^2");
            page.Paragraphs.Add(texFragment);

            // Define the rectangle area for the screen annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a screen annotation that references the TeX file as media
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, texFile);
            screenAnn.Title = "TeX Annotation";
            screenAnn.Contents = "Click to view TeX content";

            // Add the annotation to the page
            page.Annotations.Add(screenAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotation added and saved to '{outputPdf}'.");
    }
}
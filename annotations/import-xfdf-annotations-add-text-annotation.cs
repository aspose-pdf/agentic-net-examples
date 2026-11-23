using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Example XFDF content (could be loaded from a file or other source)
        string xfdfContent = @"<?xml version='1.0' encoding='UTF-8'?>
<xfdf xmlns='http://ns.adobe.com/xfdf/' xml:space='preserve'>
  <annots>
    <text page='1' rect='100,500,200,550' contents='XFDF imported note' />
  </annots>
</xfdf>";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Add a manual text annotation to the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            TextAnnotation manualAnnotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title = "Manual Note",
                Contents = "This annotation was added programmatically.",
                Open = true,
                Icon = TextIcon.Note
            };
            doc.Pages[1].Annotations.Add(manualAnnotation);

            // Import additional annotations from XFDF via a MemoryStream
            using (MemoryStream xfdfStream = new MemoryStream(Encoding.UTF8.GetBytes(xfdfContent)))
            {
                // Core API method to import XFDF annotations
                doc.ImportAnnotationsFromXfdf(xfdfStream);
            }

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with annotations to '{outputPdf}'.");
    }
}

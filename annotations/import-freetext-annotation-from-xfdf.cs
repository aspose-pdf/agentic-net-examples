using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // XFDF defining a FreeTextAnnotation with a callout line
        string xfdf = @"<?xml version='1.0' encoding='UTF-8'?>
<xfdf xmlns='http://ns.adobe.com/xfdf/'>
  <annots>
    <freeText
      page='1'
      rect='100,500,300,550'
      contents='Sample free text annotation'
      callout='true'
      calloutLine='120,520,150,540'>
    </freeText>
  </annots>
</xfdf>";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Import annotations from XFDF string
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xfdf)))
                {
                    doc.ImportAnnotationsFromXfdf(ms);
                }

                // Save the PDF with the imported annotation
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Annotations imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_annotations.pdf";

        // XFDF data as a string (replace with actual XFDF content)
        string xfdfString = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<xfdf xmlns=""http://ns.adobe.com/xfdf/"">
  <annots>
    <text page=""1"" rect=""100,500,200,550"" title=""Note"" contents=""Sample annotation""/>
  </annots>
</xfdf>";

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document, import annotations from the XFDF string, and save the result
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Convert the XFDF string to a UTF‑8 memory stream
            using (MemoryStream xfdfStream = new MemoryStream(Encoding.UTF8.GetBytes(xfdfString)))
            {
                // Import annotations from the XFDF stream into the document
                pdfDoc.ImportAnnotationsFromXfdf(xfdfStream);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdfPath}'.");
    }
}
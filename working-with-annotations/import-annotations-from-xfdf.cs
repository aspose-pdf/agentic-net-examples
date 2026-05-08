using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // XFDF data as a string (replace with your actual XFDF content)
        const string xfdfString = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<xfdf xmlns=""http://ns.adobe.com/xfdf/"">
  <annots>
    <!-- Example annotation: a text note on page 1 -->
    <text page=""1"" rect=""100,500,200,550"" name=""Note1"" flags=""Print"">
      <contents>Imported annotation text</contents>
    </text>
  </annots>
</xfdf>";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Convert the XFDF string into a memory stream (UTF‑8 encoding)
        using (MemoryStream xfdfStream = new MemoryStream(Encoding.UTF8.GetBytes(xfdfString)))
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Import all annotations from the XFDF stream into the document
                pdfDoc.ImportAnnotationsFromXfdf(xfdfStream);

                // Save the updated PDF to the specified output path
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPdfPath}'.");
    }
}
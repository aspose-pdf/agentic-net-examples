using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string pdfPath = "input.pdf";
        const string outputPath = "output.pdf";

        // XFDF data as a string (replace with actual XFDF content)
        const string xfdfString = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<xfdf xmlns=""http://ns.adobe.com/xfdf/"">
  <!-- Example annotation -->
  <annots>
    <text page=""1"" rect=""100,500,300,550"" title=""Note"" contents=""Imported annotation""/>
  </annots>
</xfdf>";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Convert the XFDF string to a UTF‑8 memory stream
        using (MemoryStream xfdfStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xfdfString)))
        {
            // Load the PDF document (lifecycle: using ensures proper disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Import annotations from the XFDF stream into the document
                doc.ImportAnnotationsFromXfdf(xfdfStream);

                // Save the updated PDF (lifecycle: save via Document.Save)
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF file
        const string outputPath = "output.pdf";      // destination PDF file
        const string xfdfData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<xfdf xmlns=""http://ns.adobe.com/xfdf/"" xml:space=""preserve"">
  <annots>
    <!-- Example annotation -->
    <highlight page=""1"" color=""#FF00FF"">
      <rects>
        <rect top=""700"" left=""100"" bottom=""720"" right=""300""/>
      </rects>
      <contents>Sample highlight</contents>
    </highlight>
  </annots>
</xfdf>";

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(pdfPath))
        {
            // Convert the XFDF string to a UTF‑8 memory stream
            using (MemoryStream xfdfStream = new MemoryStream(Encoding.UTF8.GetBytes(xfdfData)))
            {
                // Import annotations from the XFDF stream into the document
                doc.ImportAnnotationsFromXfdf(xfdfStream);
            }

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}
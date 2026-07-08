using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths
        const string pdfPath   = "input.pdf";
        const string outPath   = "output.pdf";

        // XFDF data as a string (replace with actual XFDF content)
        string xfdfString = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<xfdf xmlns=""http://ns.adobe.com/xfdf/"" xml:space=""preserve"">
  <annots>
    <!-- Example annotation -->
    <highlight page=""1"" color=""255,255,0"">
      <rect>100 500 200 520</rect>
    </highlight>
  </annots>
</xfdf>";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block)
        using (Document doc = new Document(pdfPath))
        {
            // Convert the XFDF string to a UTF‑8 memory stream
            using (MemoryStream xfdfStream = new MemoryStream(Encoding.UTF8.GetBytes(xfdfString)))
            {
                // Import annotations from the XFDF stream into the document
                doc.ImportAnnotationsFromXfdf(xfdfStream);
            }

            // Save the modified PDF (lifecycle rule: using block)
            doc.Save(outPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outPath}'.");
    }
}
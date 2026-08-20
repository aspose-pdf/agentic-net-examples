using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_annotations.pdf";

        // XFDF data as a string (replace with actual XFDF content)
        string xfdfData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<xfdf xmlns=""http://ns.adobe.com/xfdf/"" xml:space=""preserve"">
  <annots>
    <highlight page=""1"" color=""255 255 0"" />
  </annots>
</xfdf>";

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Convert the XFDF string to a memory stream (UTF-8 encoding)
            using (MemoryStream xfdfStream = new MemoryStream(Encoding.UTF8.GetBytes(xfdfData)))
            {
                // Import annotations from the XFDF stream into the document
                pdfDoc.ImportAnnotationsFromXfdf(xfdfStream);
            }

            // Save the modified PDF (lifecycle rule: save within using block)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdfPath}'.");
    }
}
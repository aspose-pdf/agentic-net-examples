using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string pdfPath = "input.pdf";
        const string outputPath = "output.pdf";

        // XFDF data as a string (replace with actual XFDF content)
        const string xfdfString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                                 "<xfdf xmlns=\"http://ns.adobe.com/xfdf/\">\n" +
                                 "  <annots>\n" +
                                 "    <text page=\"1\" rect=\"100,500,200,550\" name=\"Annot1\" flags=\"0\" title=\"Note\" subject=\"Comment\" state=\"Marked\">\n" +
                                 "      <contents>Sample annotation text.</contents>\n" +
                                 "    </text>\n" +
                                 "  </annots>\n" +
                                 "</xfdf>";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Convert the XFDF string to a UTF‑8 stream and import the annotations
            using (MemoryStream xfdfStream = new MemoryStream(Encoding.UTF8.GetBytes(xfdfString)))
            {
                pdfDoc.ImportAnnotationsFromXfdf(xfdfStream);
            }

            // Save the updated PDF (default format is PDF)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}

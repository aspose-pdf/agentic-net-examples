using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // XFDF string containing the annotations to import
        const string xfdfString = @"<?xml version='1.0' encoding='UTF-8'?>
<xfdf xmlns='http://ns.adobe.com/xfdf/'>
  <annots>
    <!-- Example annotation: a text markup annotation -->
    <textmarkup page='1' type='Highlight' rect='100,500,200,520' color='255,255,0'>
      <contents>Highlighted text</contents>
    </textmarkup>
  </annots>
</xfdf>";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Import annotations from the XFDF string into the PDF
                pdfDoc.ImportAnnotationsFromXfdf(xfdfString);

                // Save the modified PDF (PDF format, no special SaveOptions needed)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations imported successfully. Saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during loading, importing, or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

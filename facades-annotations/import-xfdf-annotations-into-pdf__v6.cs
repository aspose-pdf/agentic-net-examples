using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Sample XFDF content; replace with actual XFDF as needed
        const string xfdfContent = @"<?xml version='1.0' encoding='UTF-8'?>
<xfdf xmlns='http://ns.adobe.com/xfdf/'>
  <annots>
    <text page='1' rect='100,500,200,550' name='Comment1' flags='print' title='John Doe' subject='Note' contents='This is a note.'/>
  </annots>
</xfdf>";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // PdfAnnotationEditor does NOT implement IDisposable, so no using block
            PdfAnnotationEditor annotEditor = new PdfAnnotationEditor();

            // Bind the PDF document to the annotation editor
            annotEditor.BindPdf(pdfDoc);

            // Write XFDF content to a temporary file because ImportAnnotations expects a file path array
            string tempXfdfPath = Path.GetTempFileName();
            try
            {
                File.WriteAllText(tempXfdfPath, xfdfContent);

                // Import annotations from the XFDF file (array overload)
                annotEditor.ImportAnnotations(new[] { tempXfdfPath });
            }
            finally
            {
                // Clean up the temporary XFDF file
                if (File.Exists(tempXfdfPath))
                {
                    File.Delete(tempXfdfPath);
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with imported annotations to '{outputPath}'.");
    }
}

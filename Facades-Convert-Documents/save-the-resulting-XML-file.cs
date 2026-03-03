using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Desired output XML file path
        const string xmlPath = "output.xml";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Use a facade (PdfPageEditor) to bind the PDF document
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(pdfPath);

        // The facade exposes the underlying Document instance.
        // Dispose the Document with a using block to ensure proper cleanup.
        using (Document doc = editor.Document)
        {
            // Initialize XML save options
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the PDF as an XML file using the options
            doc.Save(xmlPath, xmlOptions);
        }

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"XML file saved to '{xmlPath}'.");
    }
}
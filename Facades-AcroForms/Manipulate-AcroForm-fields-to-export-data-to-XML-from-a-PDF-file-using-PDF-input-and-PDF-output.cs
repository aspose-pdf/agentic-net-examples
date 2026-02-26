using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF with AcroForm
        const string outputPdfPath = "output.pdf";  // PDF to be saved (unchanged)
        const string outputXmlPath = "form_data.xml"; // exported XML file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF as a Form facade – this binds the document and gives access to form operations
        using (Form form = new Form(inputPdfPath))
        {
            // Export form field data to XML (XFDF format) via ExportXml
            using (FileStream xmlStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
            }

            // Optionally save the PDF (here we simply copy it to a new file)
            form.Document.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data exported to XML: {outputXmlPath}");
        Console.WriteLine($"PDF saved to: {outputPdfPath}");
    }
}
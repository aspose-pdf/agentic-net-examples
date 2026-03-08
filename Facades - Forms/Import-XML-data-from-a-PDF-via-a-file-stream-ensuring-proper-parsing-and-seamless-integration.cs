using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (form template), the XML data file, and the output PDF.
        const string pdfTemplatePath = "template.pdf";
        const string xmlDataPath = "data.xml";
        const string outputPdfPath = "filled.pdf";

        // Verify that the required files exist before proceeding.
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Initialize the Form facade with the source PDF and the desired output PDF.
        Form form = new Form(pdfTemplatePath, outputPdfPath);

        // Open the XML file as a stream and import its field values into the PDF form.
        using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
        {
            form.ImportXml(xmlStream);
        }

        // Persist the changes to the output PDF file.
        form.Save();

        Console.WriteLine($"XML data successfully imported and saved to '{outputPdfPath}'.");
    }
}
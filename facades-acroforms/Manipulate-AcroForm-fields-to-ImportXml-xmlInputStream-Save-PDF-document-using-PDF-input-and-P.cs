using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF with AcroForm
        const string outputPdfPath = "output.pdf";  // destination PDF
        const string xmlDataPath   = "data.xml";    // XML containing field values

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Open the XML stream and the Form facade, then import and save
        using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
        using (Form form = new Form(inputPdfPath))   // bind the PDF document
        {
            // Import field values from the XML stream into the PDF form
            form.ImportXml(xmlStream);

            // Save the updated PDF to the output path
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"AcroForm fields imported from XML and saved to '{outputPdfPath}'.");
    }
}
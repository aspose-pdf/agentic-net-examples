using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF with AcroForm fields
        const string outputPdf = "output.pdf";  // PDF output (unchanged)
        const string outputXml = "form_data.xml"; // Exported XML file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF using the Form facade (Aspose.Pdf.Facades)
        using (Form form = new Form(inputPdf))
        {
            // Export the form field data to an XML stream
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
            }

            // Save the (unchanged) PDF to a new file
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form data exported to '{outputXml}'. PDF saved as '{outputPdf}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XML data file and the output PDF
        string pdfPath = "input.pdf";
        string xmlPath = "data.xml";
        string outputPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Create a Form facade and bind it to the existing PDF
        using (Form form = new Form())
        {
            form.BindPdf(pdfPath);

            // Import the XML data using a file stream
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            {
                form.ImportXml(xmlStream);
            }

            // Save the modified PDF to the specified output path
            form.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with imported XML data to '{outputPath}'.");
    }
}
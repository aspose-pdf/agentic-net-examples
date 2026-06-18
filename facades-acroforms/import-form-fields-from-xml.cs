using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input_form.pdf";   // PDF with AcroForm fields
        const string xmlPath = "data.xml";         // XML containing field values
        const string outputPath = "filled_form.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(pdfPath))
        {
            // Import field values from the XML stream
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }

            // Save the updated PDF to a new file
            form.Save(outputPath);
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";   // PDF with form fields
        const string inputXml  = "data.xml";       // XML containing field values
        const string outputPdf = "filled.pdf";     // Resulting PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(inputXml))
        {
            Console.Error.WriteLine($"XML not found: {inputXml}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(inputPdf))
        {
            // Import field values from the XML stream
            using (FileStream xmlStream = new FileStream(inputXml, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }

            // Save the updated PDF to a new file
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPdf}'.");
    }
}
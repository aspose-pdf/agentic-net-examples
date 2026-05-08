using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";
        const string xmlFile   = "fields.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }

        // Initialize the Form facade with the source PDF.
        Form form = new Form(sourcePdf);

        // Import form field values from the XML stream.
        using (FileStream xmlStream = new FileStream(xmlFile, FileMode.Open, FileAccess.Read))
        {
            form.ImportXml(xmlStream);
        }

        // Save the PDF with imported field values.
        form.Save(outputPdf);

        Console.WriteLine($"Form fields imported and saved to '{outputPdf}'.");
    }
}
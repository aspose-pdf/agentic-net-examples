using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string xmlPath = "data.xml";
        const string outputPath = "filled_form.pdf";

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

        // Use the Form facade to bind the PDF and import XML data
        using (Form form = new Form())
        {
            // Bind the source PDF
            form.BindPdf(pdfPath);

            // Import field values from the XML stream using the correct API
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            {
                form.ImportXml(xmlStream);
            }

            // Save the updated PDF to the desired output path
            form.Save(outputPath);
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPath}'.");
    }
}

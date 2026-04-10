using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "fields.xml";
        const string outputPath = "output.pdf";

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

        // Load the source PDF
        using (Document doc = new Document(pdfPath))
        {
            // Create a Form facade bound to the loaded document
            using (Form form = new Form(doc))
            {
                // Import field values from the XML stream; the original field order is retained
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXml(xmlStream);
                }

                // Save the updated PDF with imported form data
                form.Save(outputPath);
            }
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPath}'.");
    }
}
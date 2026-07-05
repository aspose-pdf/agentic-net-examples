using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string inputXmlPath  = "data.xml";
        const string outputPdfPath = "output_filled.pdf";

        // Ensure input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {inputXmlPath}");
            return;
        }

        // Open the PDF form using the Facades Form class
        using (Form form = new Form(inputPdfPath))
        {
            // Import form data from the XML file
            using (FileStream xmlStream = new FileStream(inputXmlPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }

            // Validate required fields are not empty
            foreach (string fieldName in form.FieldNames)
            {
                if (form.IsRequiredField(fieldName))
                {
                    string fieldValue = form.GetField(fieldName);
                    if (string.IsNullOrEmpty(fieldValue))
                    {
                        Console.WriteLine($"Required field '{fieldName}' is empty.");
                    }
                }
            }

            // Save the updated PDF
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}
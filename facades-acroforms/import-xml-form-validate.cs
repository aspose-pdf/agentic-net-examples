using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPdfPath = "form.pdf";
        string outputPdfPath = "filled_form.pdf";
        string xmlPath = "data.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdfPath);
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine("XML data file not found: " + xmlPath);
            return;
        }

        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }

            // Validate required fields
            foreach (string fieldName in form.FieldNames)
            {
                if (form.IsRequiredField(fieldName))
                {
                    object fieldValue = form.GetField(fieldName);
                    string valueString = fieldValue == null ? string.Empty : fieldValue.ToString();
                    if (string.IsNullOrWhiteSpace(valueString))
                    {
                        Console.WriteLine("Required field missing or empty: " + fieldName);
                    }
                }
            }

            form.Save();
        }

        Console.WriteLine("Form processing completed.");
    }
}

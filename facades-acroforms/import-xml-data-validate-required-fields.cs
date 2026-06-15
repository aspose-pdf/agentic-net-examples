using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "FormTemplate.pdf";   // PDF with AcroForm fields
        const string inputXmlPath  = "FormData.xml";       // XML containing field values
        const string outputPdfPath = "FormFilled.pdf";     // Resulting PDF

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

        // Use Form facade to bind the PDF, import XML data, validate required fields, and save.
        using (Form form = new Form())
        {
            // Bind the PDF document to the Form facade.
            form.BindPdf(inputPdfPath);

            // Import field values from the XML stream.
            using (FileStream xmlStream = File.OpenRead(inputXmlPath))
            {
                form.ImportXml(xmlStream);
            }

            // Validate required fields – log any that are empty or missing.
            foreach (string fieldName in form.FieldNames)
            {
                // Check if the field is marked as required.
                if (form.IsRequiredField(fieldName))
                {
                    // Retrieve the current value of the field.
                    object fieldValue = form.GetField(fieldName);

                    // Consider null or whitespace as a violation.
                    bool isEmpty = fieldValue == null ||
                                   string.IsNullOrWhiteSpace(fieldValue.ToString());

                    if (isEmpty)
                    {
                        Console.WriteLine($"[Violation] Required field '{fieldName}' is empty.");
                    }
                }
            }

            // Save the updated PDF to the specified output path.
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form processing completed. Output saved to '{outputPdfPath}'.");
    }
}
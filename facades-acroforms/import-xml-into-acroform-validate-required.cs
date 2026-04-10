using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "form_template.pdf";   // PDF with AcroForm fields
        const string inputXmlPath  = "form_data.xml";       // XML containing field values
        const string outputPdfPath = "form_filled_validated.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the Form facade on the loaded document
            using (Form form = new Form(pdfDoc))
            {
                // Import field values from the XML file
                using (FileStream xmlStream = File.OpenRead(inputXmlPath))
                {
                    form.ImportXml(xmlStream);
                }

                // Validate required fields – log any that are empty or missing
                foreach (string fieldName in form.FieldNames)
                {
                    // Check if the field is marked as required
                    if (form.IsRequiredField(fieldName))
                    {
                        // Retrieve the current value of the field
                        object fieldValue = form.GetField(fieldName);

                        // Consider null, empty string, or whitespace as a violation
                        bool isEmpty = fieldValue == null ||
                                       (fieldValue is string s && string.IsNullOrWhiteSpace(s));

                        if (isEmpty)
                        {
                            Console.WriteLine($"[Violation] Required field '{fieldName}' is empty.");
                        }
                    }
                }

                // Save the modified PDF (including imported data) to the output file
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdfPath}'.");
    }
}